namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventError : Error
{
    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    private EventError(ErrorCode code, string message) : base(code, message) { }
    
    public static EventError CantReadyCancelledEvent() => new EventError(ErrorCode.EventCantReadyCancelledEvent, "The event cannot be readied because it is cancelled");

    public static EventError CantReadyEventWithStartTimePriorToNow() => new EventError(ErrorCode.EventCantReadyEventWithStartTimePriorToNow, "The event cannot be readied because the start time is prior to now");
    
    public static EventError CantReadyWhenTitleIsDefault() => new EventError(ErrorCode.EventCantReadyWhenTitleIsDefault, "The event cannot be readied because the title is the default value");
}