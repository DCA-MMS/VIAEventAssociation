using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Common.Values;

public class Id<T>
{
    // - Attributes
    
    /// <summary>
    /// The value of the Id
    /// </summary>
    private Guid Value { get; set; }

    // # Constructor
    protected Id()
    {
        Value = Guid.NewGuid(); 
    }
    
    // # Factory method
    public static Id<T> Create() => new Id<T>();
    
    public static Result<Id<T>> FromString(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            return Result<Id<T>>.Success(new Id<T> { Value = guid });
        }
        return Result<Id<T>>.Failure(IdError.InvalidIdConversion());
    }
    
    // # Implicit conversion from `Id` to `Guid`
    public static implicit operator Guid(Id<T> id) => id.Value;
}