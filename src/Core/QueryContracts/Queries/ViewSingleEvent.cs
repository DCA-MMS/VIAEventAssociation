using VIAEventAssociation.Core.QueryContracts.Contract;

namespace VIAEventAssociation.Core.QueryContracts.Queries;

public abstract class ViewSingleEvent
{
    public record Query(string EventId) : IQuery<Answer>;

    public record Answer(EventInfo EventInfo, List<Guest> Guests);
    
    public record EventInfo(
        string Id,
        string Title,
        string Description,
        string Location,
        string Date,
        string Visibility,
        int NumberOfParticipants,
        int MaxParticipants);
    public record Guest(string Avatar, string FullName);
}