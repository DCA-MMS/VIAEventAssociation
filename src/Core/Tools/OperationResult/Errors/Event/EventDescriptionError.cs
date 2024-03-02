namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventDescriptionError : Error
{
    /// <inheritdoc cref="Error"/>
    public override ErrorCode Code { get; init; }
    
    /// <inheritdoc cref="Error"/>
    public override string? Message { get; init; }
    
    private EventDescriptionError(ErrorCode code, string message) : base(code, message) { }

    public static EventDescriptionError IsTooLong() => new EventDescriptionError(ErrorCode.EventDescriptionIsToolLong, "The description cannot be longer than 250 characters");
    public static EventDescriptionError NotModifiable() => new EventDescriptionError(ErrorCode.EventDescriptionNotModifiable, "The description cannot be modified");
    
    
}
