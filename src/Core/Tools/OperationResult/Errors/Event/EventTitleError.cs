namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventTitleError : Error
{
    /// <inheritdoc cref="Error"/>
    public override ErrorCode Code { get; init; }
    
    /// <inheritdoc cref="Error"/>
    public override string? Message { get; init; }
    
    private EventTitleError(ErrorCode code, string message) : base(code, message) { }
    
    /// <summary>
    /// Error for when a event title is empty
    /// </summary>
    public static EventTitleError IsEmpty() => new (ErrorCode.EventTitleIsEmpty, "The title cannot be empty, it must be between 3 and 75 characters");
    
    /// <summary>
    /// Error for when a event title is too short
    /// </summary>
    public static EventTitleError IsTooShort() => new (ErrorCode.EventTitleIsTooShort, "The title cannot be shorter than 3 characters, it must be between 3 and 75 characters");
    
    /// <summary>
    /// Error for when a event title is too long
    /// </summary>
    public static EventTitleError IsTooLong() => new (ErrorCode.EventTitleIsTooLong, "The title cannot be longer than 75 characters, it must be between 3 and 75 characters");
    
    /// <summary>
    /// Error for when a event title is not modifiable
    /// </summary>
    public static EventTitleError NotModifiable() => new (ErrorCode.EventTitleNotModifiable, "The title cannot be modified");

}