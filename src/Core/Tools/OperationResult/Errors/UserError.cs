namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;


/// <summary>
/// Symbolizes a error that is related to a user
/// </summary>
public class UserError : Error
{
    // - PROPERTIES
    
    /// <inheritdoc/>
    public override ErrorCode Code { get; init; }
    
    /// <inheritdoc/>
    public override string? Message { get; init; }
    
    // # CONSTRUCTOR
    private UserError(ErrorCode code, string message) : base(code, message) {}
    
    // * FACTORY METHODS
    /// <summary>
    /// Error for when a user is not found
    /// </summary>
    public static UserError UserNotFound() => new UserError(ErrorCode.UserNotFound, "The specified user was not found");
    
    /// <summary>
    /// Error for when a user already exists
    /// </summary>
    public static UserError UserAlreadyExists() => new UserError(ErrorCode.UserAlreadyExists, "The specified user already exists");
    
    /// <summary>
    /// Error for when a user is not longer active
    /// </summary>
    public static UserError UserIsNotActive() => new UserError(ErrorCode.UserIsNotActive, "The specified user is no longer active");
    
    /// <summary>
    /// Error for when a user is not an the creator
    /// </summary>
    /// <returns></returns>
    public static UserError UserIsNotCreator() => new UserError(ErrorCode.UserIsNotCreator, "The specified user is not the creator");
    
    /// <summary>
    /// Error for when a user is not a guest
    /// </summary>
    public static UserError UserIsNotGuest() => new UserError(ErrorCode.UserIsNotGuest, "The specified user is not a guest");
}

