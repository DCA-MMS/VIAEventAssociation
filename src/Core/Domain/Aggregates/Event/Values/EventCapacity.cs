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
    
    // EFC Constructor
    private EventCapacity() {}
    
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
            return errors;
        }

        // ? Check if the value is less than 1 or greater than 1000
        switch (value)
        {
            // ? Value can't be less than 1
            case < 5:
                errors.Add(EventCapacityError.IsLessThanFive());
                break;
            // ? Value can't be greater than 1000
            case > 50:
                errors.Add(EventCapacityError.IsGreaterThanFifty());
                break;
        }

        // * Return the list of potential errors
        return errors;
    }

    public override bool Equals(object? obj)
    {
        if (obj is EventCapacity capacity)
        {
            return capacity._value == _value;
        }

        return false;
    }
    
    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    /// <summary>
    /// Allows implicit conversion from <see cref="EventCapacity"/> to <see cref="int"/>
    /// </summary>
    /// <returns></returns>
    public static implicit operator int(EventCapacity capacity) => capacity._value;
}