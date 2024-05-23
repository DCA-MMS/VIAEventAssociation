using VIAEventAssociation.Core.QueryContracts.Contract;

namespace VIAEventAssociation.Core.QueryContracts.Queries;

public abstract class IncomingInvitations
{
    // - The Query
    public record Query(string UserId,int Offset, int Limit) : IQuery<Answer>;
    
    // - The answer
    public record Answer(List<Invitation> Invitations);
    
    // * Invitation DTO
    public record Invitation(string EventId, string EventTitle, string EventStart, int ParticipantsCount, int Capacity);
}