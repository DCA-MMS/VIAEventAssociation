using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Represents the capacity of an event.
/// </summary>
public class EventCapacity
{
    private readonly int _value;
    
    /// <summary>
    /// Private constructor for the <see cref="EventCapacity"/> class
    /// </summary>
    private EventCapacity(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EventCapacity"/> class
    /// </summary>
    /// <param name="value">Capacity of the event</param>
    /// <returns></returns>
    public static Result<EventCapacity> Create(int value)
    {
        // ? Validate the value
        var result = Validate(value);
        
        // ! If there are any errors, return a failure result
        if (result.Count > 0)
        {
            return Result<EventCapacity>.Failure(result.ToArray());
        }
        
        // * Create a new instance of the EventCapacity
        var capacity = new EventCapacity(value);
        
        // * if there are no errors, return a success result
        return capacity;
    }
    
    private static List<Error> Validate(int value)
    {
        // * Initialize the list of errors
        var errors = new List<Error>();
        
        // ? Value can't be negative
        if (value < 0)
        {
            errors.Add(EventCapacityError.IsNegative());
        }

        // ? Check if the value is less than 1 or greater than 1000
        switch (value)
        {
            // ? Value can't be less than 1
            case < 1:
                errors.Add(EventCapacityError.IsLessThanOne());
                break;
            // ? Value can't be greater than 1000
            case > 1000:
                errors.Add(EventCapacityError.IsGreaterThanAThousand());
                break;
        }

        // * Return the list of potential errors
        return errors;
    }
    
    
    /// <summary>
    /// Allows implicit conversion from <see cref="EventCapacity"/> to <see cref="int"/>
    /// </summary>
    /// <returns></returns>
    public static implicit operator int(EventCapacity capacity) => capacity._value;
}