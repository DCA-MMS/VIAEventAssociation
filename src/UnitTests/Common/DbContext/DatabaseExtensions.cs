using VIAEventAssociation.Infrastructure.EfcQueries;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

namespace Tests.Common.DBContext;

public static class DatabaseExtensions
{
    public static VeadatabaseContext Seed(this VeadatabaseContext context)
    {
        context.Users.AddRange(UserSeedFactory.CreateUsers());
        context.SaveChanges();
        return context;
    }
}