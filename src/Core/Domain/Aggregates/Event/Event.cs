using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class Event
{
    // - Attributes
    private readonly EventTitle _title;
    private readonly EventDescription _description;
    private readonly EventStatus _status;
    private readonly EventVisibility _visibility;
    
    // # Constructor
    private Event(EventTitle title, EventDescription description, EventStatus status, EventVisibility visibility)
    {
        _title = title;
        _description = description;
        _status = status;
        _visibility = visibility;
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Event"/> class
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="description">The description to use</param>
    /// <param name="status">The status to use</param>
    /// <param name="visibility">The visibility to use</param>
    /// <returns>A <see cref="Result"/> contain either the <see cref="Event"/> or errors</returns>
    public static Result<Event> Create(string title, string description, EventStatus status = EventStatus.Draft, EventVisibility visibility = EventVisibility.Private) 
    {
        // * Create the title and description
        var titleResult = EventTitle.Create(title);
        var descriptionResult = EventDescription.Create(description);

        var errors = new List<Error>();
        // ? Check if the title is valid
        if(titleResult.IsFailure)
        {
            errors.AddRange(titleResult.Errors);
        }
        
        // ? Check if the description is valid
        if(descriptionResult.IsFailure)
        {
            errors.AddRange(descriptionResult.Errors);
        }
        
        // ! If any of the title or description are invalid, return a failure result
        if(errors.Count > 0)
        {
            return Result<Event>.Failure(errors.ToArray());
        }
        
        // * Create a new instance of the Event
        var @event = new Event(titleResult, descriptionResult, status, visibility);
        
        return @event;
    }
    
    
}