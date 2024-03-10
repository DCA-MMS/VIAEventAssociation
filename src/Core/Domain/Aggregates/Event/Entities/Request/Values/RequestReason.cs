using VIAEventAssociation.Core.Tools.OperationResult;

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
        return Result<RequestReason>.Success(new RequestReason(value));
    }
}