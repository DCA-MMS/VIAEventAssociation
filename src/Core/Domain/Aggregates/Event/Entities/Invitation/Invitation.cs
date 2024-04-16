using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation;

public class Invitation
{
    public InvitationId Id { get; }
    public User Guest { get; }
    public InvitationStatus Status { get; private set; }

    // EFC Constructor
    private Invitation() {}

    private Invitation(User guest, InvitationStatus status)
    {
        Id = new InvitationId();
        Guest = guest;
        Status = status;
    }

    public static Result<Invitation> Create(User guest, InvitationStatus status)
    {
        return Result<Invitation>.Success(new Invitation(guest, status));
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