using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation;

public class Invitation
{
    public InvitationId Id { get; }
    public UserId GuestId { get; }
    public InvitationStatus Status { get; private set; }

    // EFC Constructor
    private Invitation() {}

    private Invitation(UserId guestId, InvitationStatus status)
    {
        Id = new InvitationId();
        GuestId = guestId;
        Status = status;
    }

    public static Result<Invitation> Create(UserId guestId, InvitationStatus status)
    {
        return Result<Invitation>.Success(new Invitation(guestId, status));
    }

    public Result Accept()
    {
        Status = InvitationStatus.Accepted;
        return Result.Success();
    }
    
    public Result Decline()
    {
        Status = InvitationStatus.Rejected;
        return Result.Success();
    }
}