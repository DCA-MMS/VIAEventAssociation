using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Users;

public class User
{
    public UserId Id { get; }
    public FullName FullName { get; }
    public Email Email { get; }
    public Uri? Avatar { get; private set; }
    
    // EFC Constructor
    private User() {}

    private User(UserId id, FullName fullName, Email email, Uri? avatar = null)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Avatar = avatar;
    }

    public static Result<User> Create(FullName fullName, Email email, Uri? avatar = null)
    {
        return Result<User>.Success(new User(new UserId(), fullName, email, avatar));
    }
}