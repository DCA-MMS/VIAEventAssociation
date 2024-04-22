using VIAEventAssociation.Core.QueryContracts.Contract;

namespace VIAEventAssociation.Core.QueryContracts.Queries;

public abstract class UserProfilePage
{
    public record Query(string UserId) : IQuery<Answer>;

    public record Answer(
        User User,
        int NumberOfUpcomingEvents,
        int NumberOfInvitations,
        List<UpcomingEvents> UpcomingEvents,
        List<PastEvents> PastEvents);

    public record User(string FullName, string Email, string? Avatar);

    public record UpcomingEvents(string EventId, string Title, int Attendees, string Date, string StartTime);

    public record PastEvents(string EventId, string Title);
}