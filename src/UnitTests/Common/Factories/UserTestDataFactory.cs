using VIAEventAssociation.Core.Domain.Entities.User;
using VIAEventAssociation.Core.Domain.Entities.User.Values;

namespace Tests.Common.Factories;

public static class UserTestDataFactory
{
    public static User ValidUser()
    {
        return User.Create(FullName.Create("John", "Johnson"), Email.Create("John@via.dk")).Value;
    }
}