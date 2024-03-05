namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public class TimeRangeError : Error
{
    private TimeRangeError(ErrorCode code, string message) : base(code, message)
    {
    }

    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    
    public static TimeRangeError EndBeforeOrEqualToStart() => new TimeRangeError(ErrorCode.TimeRangeEndBeforeOrEqualToStart, "The end date cannot be before or equal to the start date");  
}