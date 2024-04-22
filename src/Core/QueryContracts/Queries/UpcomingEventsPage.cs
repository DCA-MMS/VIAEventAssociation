using VIAEventAssociation.Core.QueryContracts.Contract;

namespace VIAEventAssociation.Core.QueryContracts.Queries;

public abstract class UpcomingEventsPage
{
    public record Query(int EventOffset, int EventLimit) : IQuery<Answer>;

    public record Answer(List<Event> Events, int CurrentPage, int TotalPages);

    public record Event(
        string Title,
        string Description,
        string Date,
        string StartTime,
        int Attendees,
        int MaxAttendees,
        string Visibility);
}