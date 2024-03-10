namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// The status of an event.
/// </summary>
public enum EventStatus
{
    /// <summary>
    /// The event is still being planned and is not yet ready for publication.
    /// </summary>
    Draft,
    /// <summary>
    /// The event is ready for publication and is to be visible for the public.
    /// </summary>
    Ready,
    /// <summary>
    /// The event is currently active and is visible for the public.
    /// </summary>
    Active,
    /// <summary>
    /// The event has been cancelled and is no longer visible for the public.
    /// </summary>
    Cancelled
}