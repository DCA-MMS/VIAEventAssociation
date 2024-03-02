namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

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
    /// The event is ready for publication and is visible to the public.
    /// </summary>
    Published,
    /// <summary>
    /// The event has been cancelled and is no longer visible to the public.
    /// </summary>
    Cancelled,
    /// <summary>
    /// The event has been completed and is no longer visible to the public.
    /// </summary>
    Completed,
    /// <summary>
    /// The event has been archived and is no longer visible to the public.
    /// </summary>
    Archived
}