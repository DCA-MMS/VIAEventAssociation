using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class EventFactory
{
    // - Attributes
    private readonly Event _event = Event.Create();
    private readonly List<Error> _errors = [];
    
    /// <summary>
    /// Initializes the creation of a new event
    /// </summary>
    /// <returns></returns>
    public static EventFactory Create()
    {
        return new EventFactory();
    }
    
    /// <summary>
    /// Adds a title to the event
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public EventFactory WithTitle(string title)
    {
        var result = _event.ChangeTitle(title);

        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    /// <summary>
    /// Adds a description to the event
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public EventFactory WithDescription(string description)
    {
        var result = _event.ChangeDescription(description);
        
        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    /// <summary>
    /// Adds a capacity to the event
    /// </summary>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public EventFactory WithCapacity(int capacity)
    {
        var result = _event.ChangeCapacity(capacity);
        
        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    /// <summary>
    /// Adds a time range to the event
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public EventFactory WithTimeRange(DateTime start, DateTime end)
    {
        var result = _event.ChangeTimeRange(start, end);
        
        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    /// <summary>
    /// Adds a status to the event
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public EventFactory WithStatus(EventStatus status)
    {
        _event.ChangeStatus(status);
        return this;
    }
    
    /// <summary>
    /// Adds a visibility to the event
    /// </summary>
    /// <param name="visibility"></param>
    /// <returns></returns>
    public EventFactory WithVisibility(EventVisibility visibility)
    {
        _event.ChangeVisibility(visibility);
        return this;
    }
    
    /// <summary>
    /// Returns the built event
    /// </summary>
    /// <returns></returns>
    public Result<Event> Build()
    {
        // ! If there are no errors, return the event
        if (_errors.Count <= 0) return _event;
        
        // Else, return a failure result with the errors
        var errors = _errors.ToArray();
        _errors.Clear();
        return Result<Event>.Failure(errors);

    }
}