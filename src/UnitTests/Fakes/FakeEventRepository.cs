using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Domain.Repositories;

namespace Tests.Fakes;

public class FakeEventRepository : IEventRepository
{
    public List<Event> Events { get; } =
    [
        EventFactory.Create().WithTitle("Event 1").Build(),
        EventFactory.Create().WithTitle("Event 2").Build(),
        EventFactory.Create().WithTitle("Event 3").Build(),
        EventFactory.Create().WithTitle("Event 4").Build(),
        EventFactory.Create().WithTitle("Event 5").Build()
    ];

    public Task AddAsync(Event entity)
    {
        Events.Add(entity);
        return Task.CompletedTask;
    }

    public Task<Event?> GetByIdAsync(Id<Event> id)
    {
        return Task.FromResult(Events.FirstOrDefault(e => e.Id == id));
    }
}