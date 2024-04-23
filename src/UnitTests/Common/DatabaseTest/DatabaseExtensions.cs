using VIAEventAssociation.Infrastructure.EfcQueries;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

namespace Tests.Common.DBContext;

public static class DatabaseExtensions
{
    public static VeadatabaseContext Seed(this VeadatabaseContext context)
    {
        UserSeedFactory.Seed(context);
        context.SaveChanges();
        
        EventSeedFactory.Seed(context);
        context.SaveChanges();
        
        ParticipationsSeedFactory.Seed(context);
        context.SaveChanges();
        
        return context;
    }
}