using Tests.Common.DatabaseTest.SeedFactories;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
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

    public static VeadatabaseContext AddUpcomingEvents(this VeadatabaseContext context)
    {
        // #1: Create a list of upcoming events
        var upcomingEvents = new List<Event>
        {
            new Event
            {
                Id = Guid.NewGuid().ToString() + "TEST1",
                Title = "Galactic Night Sky Gazing",
                Description = "Discover stars and planets in our night sky.",
                DurationStart = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm"),
                DurationEnd = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:mm"),
                Capacity = 30,
                Status = (int) EventStatus.Active,
                Visibility = (int) EventVisibility.Public
            },
            new Event
            {
                Id = Guid.NewGuid().ToString() + "TEST2",
                Title = "The Secret World of Spices",
                Description = "Explore the rich history and tastes of spices.",
                DurationStart = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm"),
                DurationEnd = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd HH:mm"),
                Capacity = 20,
                Status = (int) EventStatus.Active,
                Visibility = (int) EventVisibility.Public
            },
            new Event
            {
                Id = Guid.NewGuid().ToString() + "TEST3",
                Title = "Eco Warriors: Urban Gardening",
                Description = "Learn to garden in small urban spaces.",
                DurationStart = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm"),
                DurationEnd = DateTime.Now.AddDays(6).ToString("yyyy-MM-dd HH:mm"),
                Capacity = 25,
                Status = (int) EventStatus.Active,
                Visibility = (int) EventVisibility.Public
            }
        };
        
        // #2: Add the events to the context
        context.Events.AddRange(upcomingEvents);
        
        // #3: Save the changes
        context.SaveChanges();
        
        return context;
    }
}