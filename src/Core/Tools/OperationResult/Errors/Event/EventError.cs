namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventError : Error
{
    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    private EventError(ErrorCode code, string message) : base(code, message) { }
    
    public static EventError CantReadyOrActivateCancelledEvent() => new EventError(ErrorCode.EventCantReadyOrActivateCancelledEvent, "The event cannot be readied or activated because it is cancelled");

    public static EventError CantReadyOrActivateEventWithStartTimePriorToNow() => new EventError(ErrorCode.EventCantReadyOrActivateEventWithStartTimePriorToNow, "The event cannot be readied or activated because the start time is prior to now");
    
    public static EventError CantReadyOrActivateWhenTitleIsDefault() => new EventError(ErrorCode.EventCantReadyOrActivateWhenTitleIsDefault, "The event cannot be readied or activated because the title is the default value");
}