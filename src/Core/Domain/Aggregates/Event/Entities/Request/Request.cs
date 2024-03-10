using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request;

public class Request
{
    public UserId GuestId { get; }
    public RequestStatus Status { get; private set; }

    private Request(UserId guestId, RequestStatus status)
    {
        GuestId = guestId;
        Status = status;
    }

    public static Result<Request> Create(UserId guestId, RequestStatus status)
    {
        return Result<Request>.Success(new Request(guestId, status));
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