namespace VIAEventAssociation.Core.Domain.Common.Values;

public class Id<T>
{
    // - Attributes
    
    /// <summary>
    /// The value of the Id
    /// </summary>
    public Guid Value { get; private protected set; }

    // # Constructor
    protected Id()
    {
        Value = Guid.NewGuid(); 
    }
    
    // # Implicit conversion from `Id` to `Guid`
    public static implicit operator Guid(Id<T> id) => id.Value;
}