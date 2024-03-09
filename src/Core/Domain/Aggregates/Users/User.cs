using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Users;

public class User
{
    public FullName FullName { get; }
    public Email Email { get; }

    private User(FullName fullName, Email email)
    {
        FullName = fullName;
        Email = email;
    }

    public static Result<User> Create(FullName fullName, Email email)
    {
        return Result<User>.Success(new User(fullName, email));
    }
}