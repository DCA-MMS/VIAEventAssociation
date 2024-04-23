using Tests.Common.DatabaseTest.SeedFactories;
using VIAEventAssociation.Infrastructure.EfcQueries.Scaffold;

namespace Tests.Common.DatabaseTest;

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
        
        InvitationsSeedFactory.Seed(context);
        context.SaveChanges();
        
        return context;
    }
}