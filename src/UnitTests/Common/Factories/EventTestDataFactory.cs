using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace Tests.Common.Factories;

public static class EventTestDataFactory
{
    public static Event DraftEvent()
    {
        return EventFactory.Create().Build();
    }
    
    public static Event PrivateEvent()
    {
        var now = DateTime.Now;
        
        return EventFactory.Create().WithTitle("Valid Title").WithDescription("Valid description")
            .WithTimeRange(new DateTime(now.Year, now.Month, now.Day + 1, 8, 0, 0), new DateTime(now.Year, now.Month, now.Day + 1, 12, 0, 0))
            .WithCapacity(50).Build();
    }

    public static Event PublicEvent()
    {
        var @event = PrivateEvent();

        @event.ChangeVisibility(EventVisibility.Public);

        return @event;
    }
    
    public static Event ActivePublicEvent()
    {
        var publicEvent = PublicEvent();
    
        publicEvent.ChangeStatus(EventStatus.Active);
        
        return publicEvent;
    }
    
    public static Event FullActivePublicEvent()
    {
        var @event = ActivePublicEvent();
        
        for (var i = 0; i < @event.Capacity; i++)
        {
            @event.AddGuest(new UserId());
        }

        return @event;
    }
}