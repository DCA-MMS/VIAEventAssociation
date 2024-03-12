namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.User;

public class UserEmailError : Error
{
    
    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }

    private UserEmailError(ErrorCode code, string message) : base(code, message) { }

    public static UserEmailError EmailIsEmpty() => new (ErrorCode.EmailIsEmpty, "Email may not be empty");
    public static UserEmailError EmailMustStartWith() => new (ErrorCode.EmailMustStartWith, "Email must start with 6 numbers or 3-4 letters");
    public static UserEmailError EmailMustEndWith() => new (ErrorCode.EmailMustEndWith, "Email must end with @via.dk");
    public static UserEmailError EmailWithInvalidCharacters() => new (ErrorCode.EmailWithInvalidCharacters, "Email may not contain special characters or symbols");
}