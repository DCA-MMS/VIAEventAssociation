namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventErrors : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private EventErrors(ErrorCode code, string message) : base(code, message) { }
    
}