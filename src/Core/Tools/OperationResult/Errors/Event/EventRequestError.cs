namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventRequestError : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventRequestError(ErrorCode code, string message) : base(code, message) { }
    
    public static EventRequestError RequestReasonIsTooLong() => new EventRequestError(ErrorCode.RequestReasonIsTooLong, "The request reason cannot exceed 250 characters");
    
    public static EventRequestError RequestToEventThatIsNotActive() => new (ErrorCode.RequestToEventThatIsNotActive, "You can only create requests for an active event");
    
    public static EventRequestError RequestToFullEvent() => new (ErrorCode.RequestToFullEvent, "You cannot create request to an event that is full");
    
    public static EventRequestError RequestToEventThatIsNotPublic() => new (ErrorCode.RequestToEventThatIsNotPublic, "Only public events can be participated");

    public static EventRequestError RequestToEventGuestIsAlreadyPartaking() => new (ErrorCode.RequestToEventGuestIsAlreadyPartaking, "You are already attending this event");
    
    public static EventRequestError RequestToEventInThePast() => new (ErrorCode.RequestToEventInThePast, "You cannot create a request to an event that has already taken place");

}