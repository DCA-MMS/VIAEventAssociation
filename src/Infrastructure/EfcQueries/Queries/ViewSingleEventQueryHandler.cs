using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class ViewSingleEventQueryHandler(EfcDbContext context) : IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>
{
    public async Task<ViewSingleEvent.Answer> HandleAsync(ViewSingleEvent.Query query)
    {
        var eventIdResult = EventId.FromString(query.EventId);
        if (eventIdResult.IsFailure)
        {
            throw new Exception("Invalid event id format");
        }
        
        var result =  await context.Events
            .Where(e => e.Id.Value == eventIdResult.Value)
            .Include(e => e.Participants)
            .Select(e => new
            {
                EventInfo = new ViewSingleEvent.EventInfo(
                    e.Id.Value.ToString(),
                    e.Title,
                    e.Description,
                    "Location",
                    e.Duration!.Start.Date.ToString("dd-MM-yyyy"),
                    e.Visibility.ToString(),
                    e.Participants.Count,
                    e.Capacity
                ),
                Guests = e.Participants.Select(p =>
                        new ViewSingleEvent.Guest(p.Avatar != null ? p.Avatar.ToString() : "", p.FullName.ToString()))
                    .ToList(),
            }).SingleAsync();
        
        return new ViewSingleEvent.Answer(result.EventInfo, result.Guests);
    }
}