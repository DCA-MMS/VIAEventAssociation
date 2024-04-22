using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class EventEditingOverviewQueryHandler(EfcDbContext context) : IQueryHandler<EventEditingOverview.Query, EventEditingOverview.Answer>
{
    public async Task<EventEditingOverview.Answer> HandleAsync(EventEditingOverview.Query query)
    {
        var result = await context.Events
            .Where(e => e.Status == EventStatus.Draft)
            .Select(e => new
            {
                Drafts = context.Events.Where(e => e.Status == EventStatus.Draft)
                    .Select(e => new EventEditingOverview.Event(
                        e.Id.Value.ToString(),
                        e.Title
                    )).ToList(),
                Ready = context.Events.Where(e => e.Status == EventStatus.Ready)
                    .Select(e => new EventEditingOverview.Event(
                        e.Id.Value.ToString(),
                        e.Title
                    )).ToList(),
                Cancelled = context.Events.Where(e => e.Status == EventStatus.Cancelled)
                    .Select(e => new EventEditingOverview.Event(
                        e.Id.Value.ToString(),
                        e.Title
                    )).ToList(),
            }).SingleAsync();
        
        return new EventEditingOverview.Answer(result.Drafts, result.Ready, result.Cancelled);
    }
}