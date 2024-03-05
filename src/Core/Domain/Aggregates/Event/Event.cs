using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class Event
{
    // - Attributes
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStatus Status { get; private set; }
    public EventVisibility Visibility { get; private set; }
    
    // # Constructor
    private Event(EventTitle title, EventDescription description, EventStatus status, EventVisibility visibility)
    {
        Title = title;
        Description = description;
        Status = status;
        Visibility = visibility;
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Event"/> class
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="description">The description to use</param>
    /// <param name="status">The status to use</param>
    /// <param name="visibility">The visibility to use</param>
    /// <returns>A <see cref="Result"/> contain either the <see cref="Event"/> or errors</returns>
    public static Result<Event> Create(string title = "Working Title", string description = "", EventStatus status = EventStatus.Draft, EventVisibility visibility = EventVisibility.Public) 
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
    
    /// <summary>
    /// Changes the title of the <see cref="Event"/>
    /// </summary>
    /// <param name="title">Title to change to.</param>
    /// <returns>A <see cref="Result"/> representing if the title was changed.</returns>
    public Result<bool> ChangeTitle(string title)
    {
        // * Create the title
        var titleResult = EventTitle.Create(title);
        
        // ? Check if the title is valid
        if(titleResult.IsFailure)
        {
            // ! If the title is invalid, return a failure result
            return Result<bool>.Failure(titleResult.Errors.ToArray());
        }
        
        // * Set the title
        Title = titleResult;
        
        // * Return a success result
        return true;
    }
    
    /// <summary>
    /// Changes the description of the <see cref="Event"/>
    /// </summary>
    /// <param name="description">Description to change to.</param>
    /// <returns> A <see cref="Result"/> representing if the description was changed.</returns>
    public Result<bool> ChangeDescription(string description)
    {
        // * Create the description
        var descriptionResult = EventDescription.Create(description);
        
        // ? Check if the description is valid
        if(descriptionResult.IsFailure)
        {
            // ! If the description is invalid, return a failure result
            return Result<bool>.Failure(descriptionResult.Errors.ToArray());
        }
        
        // * Set the description
        Description = descriptionResult;
        
        // * Return a success result
        return true;
    }
    
    public Result<bool> ChangeStatus(EventStatus status)
    {
        Status = status;
        
        // TODO: Add logic to check if the status can be changed?
        
        return true;
    }
    
    public Result<bool> ChangeVisibility(EventVisibility visibility)
    {
        Visibility = visibility;
        
        // TODO: Add logic to check if the visibility can be changed?
        
        return true;
    }
    
}