using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class EventFactory
{
    // - Attributes
    private static Event _event = Event.Create();
    private static List<Error> _errors = new List<Error>();
    
    // # Constructor
    // To prevent instantiation
    
    public static EventFactory Create()
    {
        return new EventFactory();
    }
    
    public EventFactory WithTitle(string title)
    {
        var result = _event.ChangeTitle(title);

        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    public EventFactory WithDescription(string description)
    {
        var result = _event.ChangeDescription(description);
        
        if (result.IsFailure)
        {
            _errors.AddRange(result.Errors);
        }
        
        return this;
    }
    
    public EventFactory WithStatus(EventStatus status)
    {
        _event.ChangeStatus(status);
        return this;
    }
    
    public EventFactory WithVisibility(EventVisibility visibility)
    {
        _event.ChangeVisibility(visibility);
        return this;
    }
    
    public Result<Event> Build()
    {
        if(_errors.Count > 0)
        {
            return Result<Event>.Failure(_errors.ToArray());
        }
        
        return _event;
    }
}