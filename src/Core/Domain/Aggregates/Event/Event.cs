using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class Event
{
    // - Attributes
    public EventId Id { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStatus Status { get; private set; }
    public EventVisibility Visibility { get; private set; }
    public EventCapacity Capacity { get; private set; }
    public EventTimeRange TimeRange { get; private set; }
    public List<UserId> Participants { get; }
    
    // # Constructor
    private Event(EventTitle title, EventDescription description, EventStatus status, EventVisibility visibility, EventCapacity capacity, EventTimeRange timeRange)
    {
        Id = new EventId();
        Title = title;
        Description = description;
        Status = status;
        Visibility = visibility;
        Capacity = capacity;
        TimeRange = timeRange;
        Participants = new List<UserId>();
    }
    
    /// <summary>
    /// Creates a new instance of the <see cref="Event"/> class
    /// </summary>
    /// <param name="title">The title to use</param>
    /// <param name="description">The description to use</param>
    /// <param name="status">The status to use</param>
    /// <param name="visibility">The visibility to use</param>
    /// <param name="capacity">The capacity to use</param>
    /// <returns>A <see cref="Result"/> contain either the <see cref="Event"/> or errors</returns>
    public static Result<Event> Create(string title = "Working Title", string description = "", EventStatus status = EventStatus.Draft, EventVisibility visibility = EventVisibility.Private, int capacity = 5) 
    {
        // * Create the title, description and capacity
        var titleResult = EventTitle.Create(title);
        var descriptionResult = EventDescription.Create(description);
        var capacityResult = EventCapacity.Create(capacity);
        var timeRangeResult = EventTimeRange.Create(DateTime.Today.AddHours(8), DateTime.Today.AddHours(9));

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
        
        // ? Check if the capacity is valid
        if(capacityResult.IsFailure)
        {
            errors.AddRange(capacityResult.Errors);
        }
        
        // ! If any of the title or description are invalid, return a failure result
        if(errors.Count > 0)
        {
            return Result<Event>.Failure(errors.ToArray());
        }
        
        // * Create a new instance of the Event
        var @event = new Event(titleResult, descriptionResult, status, visibility, capacityResult,timeRangeResult);
        
        return @event;
    }
    
    /// <summary>
    /// Changes the title of the <see cref="Event"/>
    /// </summary>
    /// <param name="title">Title to change to.</param>
    /// <returns>A <see cref="Result"/> representing if the title was changed.</returns>
    public Result<bool> ChangeTitle(string title)
    {
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result<bool>.Failure(EventTitleError.NotModifiable());
        }
        
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
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
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
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result<bool>.Failure(EventDescriptionError.NotModifiable());
        }
        
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
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
        // * Return a success result
        return true;
    }

    /// <summary>
    /// Changes the capacity of the <see cref="Event"/>
    /// </summary>
    /// <param name="capacity">Capacity to change to.</param>
    /// <returns>A <see cref="Result"/> representing if the description was changed.</returns>
    public Result<bool> ChangeCapacity(int capacity)
    {
        // * Create the capacity
        var capacityResult = EventCapacity.Create(capacity);

        if (capacityResult.IsFailure)
        {
            // if the capacity is invalid, return a failure result
            return Result<bool>.Failure(capacityResult.Errors.ToArray());
        }
        
        // * Set the capacity
        Capacity = capacityResult;
        
        // * Return a success result
        return true;
    }

    /// <summary>
    /// Changes the time range of the <see cref="Event"/>
    /// </summary>
    /// <param name="start">Start date and time of the event</param>
    /// <param name="end">End date and time of the event</param>
    /// <returns></returns>
    public Result<bool> ChangeTimeRange(DateTime start, DateTime end)
    {
        // ? Check if the title is modifiable
        if(Status is EventStatus.Active or EventStatus.Cancelled)
        {
            return Result<bool>.Failure(EventDescriptionError.NotModifiable());
        }
        
        // * Create the time range
        var timeRangeResult = EventTimeRange.Create(start, end);
        
        // ? Check if the time range is valid
        if(timeRangeResult.IsFailure)
        {
            // ! If the time range is invalid, return a failure result
            return Result<bool>.Failure(timeRangeResult.Errors.ToArray());
        }
        
        // * Set the time range
        TimeRange = timeRangeResult;
        
        // * Change the status to Draft if it is not already
        if(Status != EventStatus.Draft)
        {
            Status = EventStatus.Draft;
        }
        
        // * Return a success result
        return true;
    }
    
    public Result<bool> ChangeStatus(EventStatus status)
    {
        Status = status;
        
        return true;
    }
    
    public Result<bool> ChangeVisibility(EventVisibility visibility)
    {
        if(Status is EventStatus.Cancelled)
        {
            return Result<bool>.Failure(EventVisibilityError.NotModifiable());
        }
        
        Visibility = visibility;
        
        // TODO: Add logic to check if the visibility can be changed?
        
        return true;
    }

    public Result AddGuest(UserId userId)
    {
        var errors = new List<Error>();
        
        if (IsFull())
        {
            errors.Add(EventErrors.EventIsFull());
        }

        if (Status != EventStatus.Active)
        {
            errors.Add(EventErrors.EventIsNotActive());
        }

        if (Visibility != EventVisibility.Public)
        {
            errors.Add(EventErrors.EventIsNotPublic());
        }

        if (Participants.Contains(userId))
        {
            errors.Add(EventErrors.EventDuplicateParticipant());
        }
        
        if (errors.Count > 0)
        {
            return Result.Failure(errors.ToArray());
        }
        
        Participants.Add(userId);
        return Result.Success();
    }
    
    public Result RemoveGuest(UserId userId)
    {
        Participants.Remove(userId);
        return Result.Success();
    }

    private bool IsFull()
    {
        return Participants.Count >= Capacity;
    }
    
    
}
