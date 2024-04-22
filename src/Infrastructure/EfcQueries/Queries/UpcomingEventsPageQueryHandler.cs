using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class UpcomingEventsPageQueryHandler(EfcDbContext context) : IQueryHandler<UpcomingEventsPage.Query, UpcomingEventsPage.Answer>
{
    public async Task<UpcomingEventsPage.Answer> HandleAsync(UpcomingEventsPage.Query query)
    {
        var offset = query.EventOffset;
        var limit = query.EventLimit;

        var events = await context.Events.OrderBy(x => x.Title)
            .Skip(offset)
            .Take(limit)
            .Include(e => e.Participants)
            .Where(x => x.Duration != null && x.Duration.Start > DateTime.Now && x.Status == EventStatus.Active)
            .Select(e => new UpcomingEventsPage.Event(e.Title,
                e.Description,
                e.Duration!.Start.ToString("dd/MM/yyyy"),
                e.Duration.Start.ToString("HH:mm"),
                e.Participants.Count,
                e.Capacity,
                e.Visibility.ToString()))
            .ToListAsync();
        
        var totalPages = (int)Math.Ceiling((double)events.Count / limit);          
        
        var currentPage = (int)Math.Ceiling((double)(offset / totalPages));

        return new UpcomingEventsPage.Answer(events, totalPages, currentPage);


    }
    
}