using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Entities.Request.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request;

public class Request
{
    public User Guest { get; }
    public RequestStatus Status { get; private set; }

    private Request(User guest, RequestStatus status)
    {
        Guest = guest;
        Status = status;
    }

    public static Result<Request> Create(User guest, RequestStatus status)
    {
        return Result<Request>.Success(new Request(guest, status));
    }

    public Result Approve()
    {
        Status = RequestStatus.Accepted;
        return Result.Success();
    }
    
    public Result Decline()
    {
        Status = RequestStatus.Rejected;
        return Result.Success();
    }
}