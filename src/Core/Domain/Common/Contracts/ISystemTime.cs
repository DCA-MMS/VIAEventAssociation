namespace VIAEventAssociation.Core.Domain.Common.Contracts;

/// <summary>
/// Contract for handling system time (Is used for testing purposes)
/// Has to be set on the Event
/// </summary>
public interface ISystemTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Today { get; }
}