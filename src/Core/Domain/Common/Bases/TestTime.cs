using VIAEventAssociation.Core.Domain.Common.Contracts;

namespace VIAEventAssociation.Core.Domain.Common.Bases;

public class TestTime : ISystemTime
{
    /// <summary>
    /// Allows to set the current time for testing purposes
    /// </summary>
    public DateTime Now { get; set; }
    
    /// <summary>
    /// Allows to set the current UTC time for testing purposes
    /// </summary>
    public DateTime UtcNow { get; set; }
    
    /// <summary>
    /// Allows to set the current date for testing purposes
    /// </summary>
    public DateTime Today { get; set; }
}