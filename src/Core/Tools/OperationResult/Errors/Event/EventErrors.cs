namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventErrors : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventErrors(ErrorCode code, string message) : base(code, message) { }
    
    public static EventErrors EventIsNotActive() => new (ErrorCode.EventIsNotActive, "The event is not active");
    
    public static EventErrors EventIsFull() => new (ErrorCode.EventIsFull, "The event is full");
    
    public static EventErrors EventIsNotPublic() => new (ErrorCode.EventIsNotPublic, "Only public event can be participated");

    public static EventErrors EventDuplicateParticipant() => new (ErrorCode.EventDuplicateParticipant, "You are already attending this event");

}