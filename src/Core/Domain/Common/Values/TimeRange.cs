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
        if (result.failure)
        {
            return Result<TimeRange>.Failure(result.errors.ToArray());
        }

        return new TimeRange(start, end);

    }
    
    private static (bool failure, List<Error> errors) Validate(DateTime start, DateTime end)
    {
        var errors = new List<Error>();
        if (start >= end)
        {
            errors.Add(TimeRangeError.EndBeforeOrEqualToStart());
        }
        return (errors.Count > 0, errors);
    }
}