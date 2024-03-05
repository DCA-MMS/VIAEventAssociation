using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

/// <summary>
/// Event Title
/// </summary>
public class EventTitle
{
    private readonly string _value;

    /// <summary>
    /// Private constructor for the <see cref="EventTitle"/> class
    /// </summary>
    /// <param name="value"></param>
    private EventTitle(string value)
    {
        _value = value;
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
        if (result.failure)
        {
            return Result<EventTitle>.Failure(result.errors.ToArray());
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
    private static (bool failure, List<Error> errors) Validate(string value)
    {
        // * Initialize the list of errors
        var errors = new List<Error>();

        // ? Is value null or empty?
        if (string.IsNullOrEmpty(value))
        {
            
            errors.Add(EventTitleError.IsEmpty());
            return (errors.Count > 0,errors);
        }
        
        switch (value)
        {
            // ? Is value too short?
            case { Length: < 3 }:
                errors.Add(EventTitleError.IsTooShort());
                return (errors.Count > 0,errors);
            
            // ? Is value too long?
            case { Length: > 75 }:
                errors.Add(EventTitleError.IsTooLong());
                return (errors.Count > 0,errors);
        }
         
        // * If there are no errors, return a success result
        return (errors.Count != 0,errors);;
    }
    
    /// <summary>
    /// Allows implicit conversion from <see cref="EventTitle"/> to <see cref="string"/>
    /// </summary>
    public static implicit operator string(EventTitle title) => title._value;
}