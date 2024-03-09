using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users;

namespace Tests.Common.Factories;

public static class UserTestDataFactory
{
    public static User ValidUser()
    {
        return User.Create(FullName.Create("John", "Johnson"), Email.Create("John@via.dk")).Value;
    }
}