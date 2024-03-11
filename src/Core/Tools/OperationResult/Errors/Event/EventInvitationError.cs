namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventInvitationError : Error
{
    
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventInvitationError(ErrorCode code, string message) : base(code, message) { }

    public static EventInvitationError InvitationToNonReadyOrActiveEvent() => new (ErrorCode.InvitationToNonReadyOrActiveEvent, "Guests can only be invited to ready or active events");
    
    public static EventInvitationError InvitationToFullEvent() => new (ErrorCode.InvitationToFullEvent, "You cannot invite a guest to a full event");

}