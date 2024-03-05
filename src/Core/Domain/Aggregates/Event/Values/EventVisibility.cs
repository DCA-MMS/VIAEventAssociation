namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// The visibility of an event.
/// </summary>
public enum EventVisibility
{
    /// <summary>
    /// The event is visible to the public.
    /// </summary>
    Public,
    /// <summary>
    /// The event is not visible to the public and is invite-only.
    /// </summary>
    Private
}