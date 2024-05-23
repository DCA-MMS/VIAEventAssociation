using Microsoft.EntityFrameworkCore;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.QueryContracts.Contract;
using VIAEventAssociation.Core.QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcDmPersistence;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace VIAEventAssociation.Infrastructure.EfcQueries.Queries;

public class UpcomingEventsPageQueryHandler(VeadatabaseContext context) : IQueryHandler<UpcomingEventsPage.Query, UpcomingEventsPage.Answer>
{
    public async Task<UpcomingEventsPage.Answer> HandleAsync(UpcomingEventsPage.Query query)
    {
        var offset = query.EventOffset;
        var limit = query.EventLimit;

        // Fetch all events and then filter in-memory
        var allEvents = await context.Events
            .Include(e => e.Participants)
            .ToListAsync();

        var filteredEvents = allEvents
            .Where(x => x.DurationStart != null && DateTime.Parse(x.DurationStart) > DateTime.Now && x.Status == (int)EventStatus.Active)
            .OrderBy(x => x.Title)
            .Skip(offset)
            .Take(limit)
            .Select(e => new UpcomingEventsPage.Event(
                e.Title,
                e.Description,
                DateTime.Parse(e.DurationStart!).ToString("dd/MM/yyyy"),
                DateTime.Parse(e.DurationStart!).ToString("HH:mm"),
                e.Participants.Count,
                e.Capacity,
                e.Visibility.ToString()))
            .ToList();

        var totalEventsCount = filteredEvents.Count;
        var totalPages = (int)Math.Ceiling((double)totalEventsCount / limit);
        var currentPage = offset / limit + 1;

        return new UpcomingEventsPage.Answer(filteredEvents, totalPages, currentPage);
    }
    
}