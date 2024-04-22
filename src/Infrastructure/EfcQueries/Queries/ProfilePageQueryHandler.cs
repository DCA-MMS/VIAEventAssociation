using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class ProfilePageQueryHandler(EfcDbContext context) : IQueryHandler<UserProfilePage.Query, UserProfilePage.Answer>
{
    public async Task<UserProfilePage.Answer> HandleAsync(UserProfilePage.Query query)
    {
        var userId = query.UserId;

        var result = await context.Users
            .Where(u => u.Id.Value.ToString() == userId)
            .Select(u => new
            {
                User = new UserProfilePage.User(u.FullName.ToString(), u.Email.Value,
                    u.Avatar != null ? u.Avatar.ToString() : null),
                UpcomingEvents = context.Events.OrderBy(x => x.Duration!.Start)
                    .Where(e => e.Participants.Any(p =>
                        p.Id.Value.ToString() == userId && e.Duration!.Start > DateTime.Now))
                    .Include(e => e.Participants)
                    .Select(e => new UserProfilePage.UpcomingEvents(e.Id.Value.ToString(), e.Title,
                        e.Participants.Count, e.Duration!.Start.ToString("dd/MM/yyyy"),
                        e.Duration.Start.ToString("HH:mm")))
                    .ToList(),
                PastEvents = context.Events.OrderByDescending(x => x.Duration!.Start)
                    .Include(e => e.Participants)
                    .Where(e => e.Participants.Any(
                        p => p.Id.Value.ToString() == userId && e.Duration!.End < DateTime.Now)).Select(x =>
                        new UserProfilePage.PastEvents(x.Id.Value.ToString(), x.Title))
                    .ToList(),
                NumberOfInvitations = context.Events.Include(x => x.Invitations)
                    .Count(e => e.Invitations.Any(i => i.Id.Value.ToString() == userId))
            })
            .SingleAsync();
        
        return new UserProfilePage.Answer(result.User, result.UpcomingEvents.Count, result.NumberOfInvitations, result.UpcomingEvents, result.PastEvents);
    }
}