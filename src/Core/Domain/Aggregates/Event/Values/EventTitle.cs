using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Event Title
/// </summary>
public class EventTitle
{
    public string Value { get; }

    // EFC Constructor
    private EventTitle() {}

    /// <summary>
    /// Private constructor for the <see cref="EventTitle"/> class
    /// </summary>
    /// <param name="value"></param>
    private EventTitle(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EventTitle"/> class
    /// If value is invalid, a failure result will be returned
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <returns>A success result, if the value was valid. Otherwise a failure result with the corresponding error</returns>
    public static Result<EventTitle> Create(string value)
    {
        // ? Validate the value
        var result = Validate(value);

        // ! If there are any errors, return a failure result
        if (result.Count > 0)
        {
            return Result<EventTitle>.Failure(result.ToArray());
        }

        // * Create a new instance of the EventDescription
        var description = new EventTitle(value);

        // * If there are no errors, return a success result
        return description;
    }

    /// <summary>
    /// Validates the event title.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static List<Error> Validate(string value)
    {
        // * Initialize the list of errors
        var errors = new List<Error>();

        // ? Is value null or empty?
        if (string.IsNullOrEmpty(value))
        {
            errors.Add(EventTitleError.IsEmpty());
        }
        
        switch (value)
        {
            // ? Is value too short?
            case { Length: < 3 }:
                errors.Add(EventTitleError.IsTooShort());
                break;
            // ? Is value too long?
            case { Length: > 75 }:
                errors.Add(EventTitleError.IsTooLong());
                break;;
        }
         
        // * If there are no errors, return a success result
        return errors;
    }

    public override bool Equals(object? obj)
    {
        if (obj is EventTitle title)
        {
            return title.Value == Value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Allows implicit conversion from <see cref="EventTitle"/> to <see cref="string"/>
    /// </summary>
    public static implicit operator string(EventTitle title) => title.Value;
}