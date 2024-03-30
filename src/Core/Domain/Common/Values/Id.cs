namespace VIAEventAssociation.Core.Domain.Common.Values;

public class Id<T>
{
    // - Attributes
    
    /// <summary>
    /// The value of the Id
    /// </summary>
    public Guid Value { get; protected set; }

    // # Constructor
    protected Id()
    {
        Value = Guid.NewGuid(); 
    }
}