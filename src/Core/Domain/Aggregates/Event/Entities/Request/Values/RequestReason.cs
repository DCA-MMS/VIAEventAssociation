using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request.Values;

public class RequestReason
{
    public string Value { get; }

    private RequestReason(string value)
    {
        Value = value;
    }

    public static Result<RequestReason> Create(string value)
    {
        var errors = Validate(value);

        if (errors.Count > 0)
        {
            return Result<RequestReason>.Failure(errors.ToArray());
        }
        
        return Result<RequestReason>.Success(new RequestReason(value));
    }

    private static List<Error> Validate(string value)
    {
        var errors = new List<Error>();

        if (value.Length > 250)
        {
            errors.Add(EventRequestReasonError.RequestReasonIsTooLong());
        }
        
        return errors;
    }
}