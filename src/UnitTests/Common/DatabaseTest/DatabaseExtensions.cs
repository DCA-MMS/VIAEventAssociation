using VIAEventAssociation.Infrastructure.EfcQueries;
using VIAEventAssociation.Infrastructure.EfcQueries.SeedFactories;

namespace Tests.Common.DBContext;

public static class DatabaseExtensions
{
    public static VeadatabaseContext Seed(this VeadatabaseContext context)
    {
        context.Users.AddRange(UserSeedFactory.CreateUsers());
        context.SaveChanges();
        
        context.Events.AddRange(EventSeedFactory.CreateEvents());
        context.SaveChanges();
        
        ParticipationsSeedFactory.Seed(context);
        context.SaveChanges();
        
        return context;
    }
}