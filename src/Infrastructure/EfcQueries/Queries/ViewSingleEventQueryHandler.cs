using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class ViewSingleEventQueryHandler(VeadatabaseContext context) : IQueryHandler<ViewSingleEvent.Query, ViewSingleEvent.Answer>
{
    public async Task<ViewSingleEvent.Answer> HandleAsync(ViewSingleEvent.Query query)
    {
        // #1: Validate the query
        var correctIdResult = EventId.FromString(query.EventId);
        
        // ? If the id is invalid, throw an exception
        if (correctIdResult.IsFailure)
        {
            throw new Exception("Invalid event id format");
        }

        // #1a: Extract the id
        var eventId = correctIdResult.Value.Value.ToString();
        
        // #2: Query the database
        var result =  await context.Events
            .Where(e => e.Id == eventId)
            .Include(e => e.Participants)
            .Select(e => new
            {
                EventInfo = new ViewSingleEvent.EventInfo(
                    e.Id,
                    e.Title,
                    e.Description,
                    "Location",
                    e.DurationStart!,                    
                    e.Visibility.ToString(),
                    e.Participants.Count,
                    e.Capacity
                ),
                Guests = e.Participants.Select(p =>
                        new ViewSingleEvent.Guest(p.Avatar != null ? p.Avatar.ToString() : "", $"{p.FirstName} {p.LastName}"))
                    .ToList(),
            }).SingleAsync();
        
        // #3: Return the result
        return new ViewSingleEvent.Answer(result.EventInfo, result.Guests);
    }
}