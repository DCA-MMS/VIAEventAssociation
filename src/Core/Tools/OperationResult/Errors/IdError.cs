namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public class IdError : Error
{
    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    private IdError(ErrorCode code, string message) : base(code, message) { }
    
    public static IdError InvalidIdConversion() => new IdError(ErrorCode.InvalidIdConversion, "Conversion from string to Guid failed");
}