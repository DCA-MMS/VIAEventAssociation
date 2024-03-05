namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.User;

public class UserFullNameError : Error
{
    public override ErrorCode Code { get; init; }
    
    public override string? Message { get; init; }
    
    private UserFullNameError(ErrorCode code, string message) : base(code, message) { }

    public static UserFullNameError FirstNameIsEmpty() => new (ErrorCode.FirstNameIsEmpty, "First name cannot be empty");

    public static UserFullNameError FirstNameIsTooShort() => new (ErrorCode.FirstNameIsTooShort, "First name is too short");

    public static UserFullNameError FirstNameIsTooLong() => new (ErrorCode.FirstNameIsTooLong, "First name is too long");

    public static UserFullNameError FirstNameIsInvalid() => new (ErrorCode.FirstNameIsInvalid, "First name is invalid");

    public static UserFullNameError LastNameIsEmpty() => new (ErrorCode.LastNameIsEmpty, "Last name cannot be empty");

    public static UserFullNameError LastNameIsTooShort() => new (ErrorCode.LastNameIsTooShort, "Last name is too short");

    public static UserFullNameError LastNameIsTooLong() => new (ErrorCode.LastNameIsTooLong, "Last name is too long");

    public static UserFullNameError LastNameIsInvalid() => new (ErrorCode.LastNameIsInvalid, "Last name is invalid");}