using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Common.Bases;
using VIAEventAssociation.Core.Domain.Common.Contracts;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public static class Constants
{
    // - Event defaults
    public static readonly EventTitle DefaultEventTitle = EventTitle.Create("Working Title");
    public static readonly EventDescription DefaultEventDescription = EventDescription.Create(""); 
    public static readonly EventCapacity DefaultEventCapacity = EventCapacity.Create(5);
    public const EventStatus DefaultEventStatus = EventStatus.Draft;
    public const EventVisibility DefaultEventVisibility = EventVisibility.Private;
    public static readonly ISystemTime DefaultSystemTime = new ActualTime();
    
    
    /// <summary>
    /// Creates a system time for testing purposes:
    /// Now = 7:55
    /// UtcNow = 7:55
    /// Today = Today
    /// </summary>
    /// <returns></returns>
    public static ISystemTime GetTestSystemTime()
    {
        return new TestTime
        {
            Now = DateTime.Today.AddHours(7).AddMinutes(55),
            UtcNow = DateTime.Today.AddHours(7).AddMinutes(55).ToUniversalTime(),
            Today = DateTime.Today
        };
    }

}