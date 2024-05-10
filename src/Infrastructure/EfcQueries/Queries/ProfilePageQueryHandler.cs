using System.Globalization;
using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class ProfilePageQueryHandler(VeadatabaseContext context) : IQueryHandler<UserProfilePage.Query, UserProfilePage.Answer>
{
    public async Task<UserProfilePage.Answer> HandleAsync(UserProfilePage.Query query)
    {
        var userId = query.UserId;
        var now = DateTime.Now;

        // #1: Fetch the user profile
        var userProfile = await context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserProfilePage.User($"{u.FirstName} {u.LastName}", u.Email, u.Avatar))
            .SingleOrDefaultAsync();

        // #2: Fetch all events that the user is participating in
        var userEvents = await context.Events
            .Where(e => e.Participants.Any(p => p.Id == userId))
            .Select(e => new
            {
                e.Id,
                e.Title,
                ParticipantsCount = e.Participants.Count,
                DurationStart = e.DurationStart,
                DurationEnd = e.DurationEnd
            })
            .ToListAsync();
        
        // #3: Fetch the upcoming events and project them
        var upcomingEventsProjection = userEvents
            .Where(e => DateTime.Parse(e.DurationStart!) >= now) // Direct DateTime comparison
            .OrderBy(e => e.DurationStart)
            .Select(e => new UserProfilePage.UpcomingEvents(
                e.Id, 
                e.Title, 
                e.ParticipantsCount, 
                e.DurationStart, 
                e.DurationEnd
            ))
            .ToList();

        // #4: Fetch the past events and project them
        var pastEventsProjection = userEvents
            .Where(e => DateTime.Parse(e.DurationEnd!) < now) // Ensure these are past
            .OrderByDescending(e => e.DurationStart)
            .Select(e => new UserProfilePage.PastEvents(
                e.Id, 
                e.Title
            ))
            .ToList();

        // #5: Fetch the number of invitations the user has
        var numberOfInvitations = await context.Events
            .SelectMany(e => e.Invitations)
            .CountAsync(i => i.GuestId == userId); // Assuming InviteeId is the correct FK to user in Invitations

        // #6: Return the answer
        var result = new
        {
            User = userProfile,
            UpcomingEvents = upcomingEventsProjection,
            PastEvents = pastEventsProjection,
            NumberOfInvitations = numberOfInvitations
        };
        
        
        return new UserProfilePage.Answer(result.User!, result.UpcomingEvents.Count, result.NumberOfInvitations, result.UpcomingEvents, result.PastEvents);
    }
}