using VIAEventAssociation.Core.Domain.Common.Contracts;

namespace VIAEventAssociation.Core.Domain.Common.Bases;

public class ActualTime : ISystemTime
{
    public DateTime Now { get; } = DateTime.Now;
    public DateTime UtcNow { get; } = DateTime.UtcNow;
    public DateTime Today { get; } = DateTime.Today;
}