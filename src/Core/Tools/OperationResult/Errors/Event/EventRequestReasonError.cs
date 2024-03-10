namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventRequestReasonError : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventRequestReasonError(ErrorCode code, string message) : base(code, message) { }
    
    public static EventRequestReasonError RequestReasonIsTooLong() => new EventRequestReasonError(ErrorCode.RequestReasonIsTooLong, "The request reason cannot exceed 250 characters");
    
}