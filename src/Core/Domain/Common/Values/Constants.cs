using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public static class Constants
{
    // - Event defaults
    public static readonly EventTitle DefaultEventTitle = EventTitle.Create("Working Title");
    public static readonly EventDescription DefaultEventDescription = EventDescription.Create(""); 
    public static readonly EventCapacity DefaultEventCapacity = EventCapacity.Create(5);
    public const EventStatus DefaultEventStatus = EventStatus.Draft;
    public const EventVisibility DefaultEventVisibility = EventVisibility.Private;
}