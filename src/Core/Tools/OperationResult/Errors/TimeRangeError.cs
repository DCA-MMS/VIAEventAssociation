namespace VIAEventAssociation.Core.Tools.OperationResult.Errors;

public class TimeRangeError : Error
{
    private TimeRangeError(ErrorCode code, string message) : base(code, message)
    {
    }

    public override ErrorCode Code { get; init; }
    public override string? Message { get; init; }
    
    public static TimeRangeError EndBeforeOrEqualToStart() => new TimeRangeError(ErrorCode.TimeRangeEndBeforeOrEqualToStart, "The end date cannot be before or equal to the start date");  
    
    /// <summary>
    /// Error for when the start date is after the end date
    /// </summary>
    /// <returns></returns>
    public static TimeRangeError StartAfterEndDate() => new (ErrorCode.TimeRangeStartAfterEndDate, "The start date cannot be after the end date");
    
    /// <summary>
    /// Error for when the start time is after the end time
    /// </summary>
    /// <returns></returns>
    public static TimeRangeError StartAfterEndTime() => new (ErrorCode.TimeRangeStartAfterEndTime, "The start time cannot be after the end time");

    
}