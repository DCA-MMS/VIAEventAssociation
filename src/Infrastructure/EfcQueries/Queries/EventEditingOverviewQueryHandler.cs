using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class EventEditingOverviewQueryHandler(VeadatabaseContext context) : IQueryHandler<EventEditingOverview.Query, EventEditingOverview.Answer>
{
    public async Task<EventEditingOverview.Answer> HandleAsync(EventEditingOverview.Query query)
    {
        // #1: Fetch all events that are in draft status
        var eventDrafts = await context.Events
            .Where(e => e.Status == (int) EventStatus.Draft)
            .Select(e => new EventEditingOverview.Event(
                e.Id,
                e.Title
            )).ToListAsync();
        
        // #2: Fetch all events that are ready
        var readyEvents = await context.Events
            .Where(e => e.Status == (int) EventStatus.Ready)
            .Select(e => new EventEditingOverview.Event(
                e.Id,
                e.Title
            )).ToListAsync();
        
        // #3: Fetch all events that are cancelled
        var cancelledEvents = await context.Events
            .Where(e => e.Status == (int) EventStatus.Cancelled)
            .Select(e => new EventEditingOverview.Event(
                e.Id,
                e.Title
            )).ToListAsync();
        
        // #4: Return the answer
        return new EventEditingOverview.Answer(eventDrafts, readyEvents, cancelledEvents);
    }
}