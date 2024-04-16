using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

public class UserId : Id<User>
{
    public static Result<UserId> FromString(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            return Result<UserId>.Success(new UserId { Value = guid });
        }
        return Result<UserId>.Failure(IdError.InvalidIdConversion());
    }
    
    public static UserId FromGuid(Guid guid) => new() { Value = guid };
}