using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class TimeRange
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    private TimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static Result<TimeRange> Create(DateTime start, DateTime end)
    {
        var result = Validate(start, end);
        
        if (result.Count > 0)
        {
            return Result<TimeRange>.Failure(result.ToArray());
        }

        return new TimeRange(start, end);

    }
    
    private static List<Error> Validate(DateTime start, DateTime end)
    {
        var errors = new List<Error>();
        
        // ? Start date is after end date
        if (start.Date > end.Date)
        {
            errors.Add(TimeRangeError.StartAfterEndDate());
        }
        
        // ? Start time is after end time
        if (start.TimeOfDay > end.TimeOfDay)
        {
            errors.Add(TimeRangeError.StartAfterEndTime());
        }
        
        return errors;
    }
}