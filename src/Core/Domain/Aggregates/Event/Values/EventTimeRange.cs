using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Represents the time range of an event.
/// </summary>
public class EventTimeRange
{
    private readonly TimeRange _value;
    
    public DateTime Start => _value.Start;
    
    public DateTime End => _value.End;

    private EventTimeRange(DateTime start, DateTime end)
    {
        _value = TimeRange.Create(start,end);
    }

    public static Result<EventTimeRange> Create(DateTime start, DateTime end)
    {
        // ? Validate the value
        var result = Validate(start, end);
        
        // ! If there are any errors, return a failure result
        if(result.Count > 0)
        {
            return Result<EventTimeRange>.Failure(result.ToArray());
        }
        
        // * Create a new instance of the EventTimeRange
        var timeRange = new EventTimeRange(start, end);
        
        // * If there are no errors, return a success result
        return timeRange;
    }

    private static List<Error> Validate(DateTime start, DateTime end)
    {
        // * Create a list to store the errors
        var errors = new List<Error>();
        
        // ? Is Start date in the past?
        if(start < DateTime.Today)
        {
            errors.Add(EventTimeRangeError.StartIsInPast());
            return errors;
        }
        
        // ? Start date after end date
        if(start.Date > end.Date)
        {
            errors.Add(EventTimeRangeError.StartAfterEndDate());
            return errors;
        }
        
        // ? Start time before 08:00
        if(start.TimeOfDay < TimeSpan.FromHours(8))
        {
            errors.Add(EventTimeRangeError.StartBeforeEight());
            return errors;
        }
        
        // ? Start time after end time
        if(start.TimeOfDay > end.TimeOfDay)
        {
            errors.Add(EventTimeRangeError.StartAfterEndTime());
            return errors;
        }
        
        var duration = end - start;
        
        // ? Duration less than 1 hour
        if(duration < TimeSpan.FromHours(1))
        {
            errors.Add(EventTimeRangeError.DurationLessThanOneHour());
            return errors;
        }
        
        // ? Duration longer than 10 hours
        if(duration > TimeSpan.FromHours(10))
        {
            errors.Add(EventTimeRangeError.DurationIsLongerThanTenHours());
            return errors;
        }
        
        return errors;
    }
}