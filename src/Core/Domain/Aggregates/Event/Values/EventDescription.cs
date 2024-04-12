using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Event Description
/// </summary>
public class EventDescription
{
    public string Value { get; }

    // EFC Constructor
    private EventDescription() {}
    
    /// <summary>
    /// Private constructor for the <see cref="EventDescription"/> class
    /// </summary>
    /// <param name="value"></param>
    private EventDescription(string value)
    {      
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EventDescription"/> class
    /// If value is invalid, a failure result will be returned
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <returns>A success result, if the value was valid. Otherwise a failure result with the corresponding error</returns>
    public static Result<EventDescription> Create(string value)
    {
        // ? Validate the value
        var result = Validate(value);

        // ! If there are any errors, return a failure result
        if (result.Count > 0)
        {
            return Result<EventDescription>.Failure(result.ToArray());
        }
        
        // * Create a new instance of the EventDescription
        var description = new EventDescription(value);
        
        // * If there are no errors, return a success result
        return description;
    }
    
    /// <summary>
    /// Validates the event description.
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>A <see cref="bool"/> representing if value has passed the validation</returns>
    private static List<Error> Validate(string value)
    {
        var errors = new List<Error>();

        switch (value)
        {
            // ? Is value too long?
            case { Length: > 250 }:
                errors.Add(EventDescriptionError.IsTooLong());
                break;
        }

        return errors;
    }

    public override bool Equals(object? obj)
    {
        if (obj is EventDescription description)
        {
            return description.Value == Value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Allows for the implicit conversion of a <see cref="EventDescription"/> to a <see cref="string"/>
    /// </summary>
    public static implicit operator string(EventDescription description) => description.Value;
}