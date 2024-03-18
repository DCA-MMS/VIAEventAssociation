namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventVisibilityError : Error
{
    /// <inheritdoc cref="Error"/>
    public override ErrorCode Code { get; init; }
    /// <inheritdoc cref="Error"/>
    public override string? Message { get; init; }
    
    private EventVisibilityError(ErrorCode code, string message) : base(code, message) { }

    public static EventVisibilityError NotModifiable() => new (ErrorCode.EventVisibilityNotModifiable, "The status cannot be modified");
    
}