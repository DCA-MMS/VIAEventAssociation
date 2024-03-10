using VIAEventAssociation.Core.Domain.Common.Values;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Strongly typed Id for the Event aggregate
/// </summary>
public class EventId : Id<Event>
{
    // No additional attributes
}