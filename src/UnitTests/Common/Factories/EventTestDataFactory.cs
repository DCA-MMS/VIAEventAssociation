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

        @event.MakePublic();

        return @event;
    }
    
    public static Event ActivePublicEvent()
    {
        var publicEvent = PublicEvent();
    
        publicEvent.Activate();
        
        return publicEvent;
    }
    
    public static Event ActivePublicEventWithPendingInvitation()
    {
        var publicEvent = ActivePublicEvent();

        publicEvent.InviteGuest(new UserId());

        return publicEvent;
    }
    
    public static Event CancelledPublicEventWithPendingInvitation()
    {
        var publicEvent = ActivePublicEventWithPendingInvitation();
    
        publicEvent.Cancel();
        
        return publicEvent;
    }
    
    public static Event ReadyPublicEventWithPendingInvitation()
    {
        var publicEvent = ActivePublicEventWithPendingInvitation();
    
        publicEvent.MakeReady();
        
        return publicEvent;
    }

    public static Event ActivePublicEventWithStartTimeInPast()
    {
        var yesterday = DateTime.Today.AddDays(-1);

        return EventFactory.Create().WithVisibility(EventVisibility.Public)
            .WithTimeRange(yesterday.AddHours(8), yesterday.AddHours(12)).WithStatus(EventStatus.Active).Build();
    }
    
    public static Event ActivePublicEventWithGuestAndStartTimeInPast()
    {
        var yesterday = DateTime.Today.AddDays(-1);

        return EventFactory.Create().WithVisibility(EventVisibility.Public)
            .WithTimeRange(yesterday.AddHours(8), yesterday.AddHours(12)).WithStatus(EventStatus.Active)
            .WithParticipants(new UserId()).Build();
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
    
    public static Event FullActivePublicEventWithPendingInvitation()
    {
        var @event = ActivePublicEvent();
        
        @event.InviteGuest(new UserId());

        for (var i = 0; i < @event.Capacity; i++)
        {
            @event.AddGuest(new UserId());
        }        
        
        return @event;
    }
}