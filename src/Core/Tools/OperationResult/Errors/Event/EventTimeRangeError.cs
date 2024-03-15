namespace VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

public class EventTimeRangeError : Error
{
    /// <inheritdoc cref="Error"/>
    public override ErrorCode Code { get; init; }
    /// <inheritdoc cref="Error"/>
    public override string? Message { get; init; }
    
    private EventTimeRangeError(ErrorCode code, string message) : base(code, message) { }
    
    /// <summary>
    /// Error for when the duration is less than 1 hour
    /// </summary>
    /// <returns></returns>
    public static EventTimeRangeError DurationLessThanOneHour() => new (ErrorCode.EventTimeRangeDurationLessThanOneHour, "The duration cannot be less than 1 hour");
    
    /// <summary>
    /// Error for when the duration is longer than 10 hours
    /// </summary>
    /// <returns></returns>
    public static EventTimeRangeError DurationIsLongerThanTenHours() => new (ErrorCode.EventTimeRangeDurationIsLongerThanTenHours, "The duration cannot be longer than 10 hours");
    
    /// <summary>
    /// Error for when the start time is before 08:00
    /// </summary>
    /// <returns></returns>
    public static EventTimeRangeError StartBeforeEight() => new (ErrorCode.EventTimeRangeStartIsBeforeEight, "The start time cannot be before 08:00");
    
    /// <summary>
    /// Error for when the start date is in the past
    /// </summary>
    /// <returns></returns>
    public static EventTimeRangeError StartIsInPast() => new (ErrorCode.EventTimeRangeStartIsInPast, "The start date cannot be in the past");
    
    /// <summary>
    /// Error for when the time range is not modifiable
    /// </summary>
    /// <returns></returns>
    public static EventTimeRangeError NotModifiable() => new (ErrorCode.EventTimeRangeNotModifiable, "The time range cannot be modified");
}