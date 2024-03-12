namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventInvitationError : Error
{
    
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventInvitationError(ErrorCode code, string message) : base(code, message) { }

    public static EventInvitationError InvitationToNonReadyOrActiveEvent() => new (ErrorCode.InvitationToNonReadyOrActiveEvent, "Guests can only be invited to ready or active events");
    
    public static EventInvitationError InvitationToFullEvent() => new (ErrorCode.InvitationToFullEvent, "You cannot invite a guest to a full event");
    public static EventInvitationError InvitationAcceptToGuestNotInvited() => new (ErrorCode.InvitationAcceptToGuestNotInvited, "You are not invited to this event");
    public static EventInvitationError InvitationAcceptToFullEvent() => new (ErrorCode.InvitationAcceptToFullEvent, "You cannot accept the invitation the event is already full");
    public static EventInvitationError InvitationAcceptToCancelledEvent() => new (ErrorCode.InvitationAcceptToCancelledEvent, "You cannot accept the invitation, the event has been cancelled");
    public static EventInvitationError InvitationAcceptToReadyEvent() => new (ErrorCode.InvitationAcceptToReadyEvent, "You have to wait until the event is active before accepting the invitation");
    public static EventInvitationError InvitationDeclineToGuestNotInvited() => new (ErrorCode.InvitationDeclineToGuestNotInvited, "You are not invited to this event");
    public static EventInvitationError InvitationDeclineToCancelledEvent() => new (ErrorCode.InvitationDeclineToCancelledEvent, "You cannot decline the invitation, the event has been cancelled");
    public static EventInvitationError InvitationDeclineToReadyEvent() => new (ErrorCode.InvitationDeclineToReadyEvent, "You have to wait until the event is active before declining the invitation");
}