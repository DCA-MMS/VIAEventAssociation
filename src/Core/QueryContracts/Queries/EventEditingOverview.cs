using VIAEventAssociation.Core.QueryContracts.Contract;

namespace VIAEventAssociation.Core.QueryContracts.Queries;

public abstract class EventEditingOverview
{
    public record Query() : IQuery<Answer>;

    public record Answer(List<Event> Drafts, List<Event> Ready, List<Event> Cancelled);

    public record Event(string Id, string Title);
}