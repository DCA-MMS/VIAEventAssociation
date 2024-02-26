namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;


/// <summary>
/// A Generic Error
/// </summary>
public abstract class Error
{
    // - PROPERTIES
    
    /// <summary>
    /// The error code - see <see cref="ErrorCode"/>
    /// </summary>
    public abstract ErrorCode Code { get; init; }

    /// <summary>
    /// The error message - A human readable message
    /// </summary>
    public abstract string? Message { get; init; }
    
    // # CONSTRUCTOR
    protected Error(ErrorCode code, string message)
    {
        Code = code;
        Message = message;
    }
}