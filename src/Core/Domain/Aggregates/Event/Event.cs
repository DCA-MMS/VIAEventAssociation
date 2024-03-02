using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Aggregates.Event;

public class Event
{
    private readonly EventTitle _title;
    private readonly EventDescription _description;
    private readonly EventStatus _status;
    private readonly EventVisibility _visibility;
    
    private Event(EventTitle title, EventDescription description, EventStatus status, EventVisibility visibility)
    {
        _title = title;
        _description = description;
        _status = status;
        _visibility = visibility;
    }
    
    public static Result<Event> Create(EventTitle title, EventDescription description, EventStatus status, EventVisibility visibility)
    {
        // TODO: Validate the parameters.
        /* var result = Validate(title, description, status, visibility);
        
        if (result.failure)
        {
            return Result<Event>.Failure(result.errors.ToArray());
        }
        */
        
        var @event = new Event(title, description, status, visibility);
        
        return @event;
    }
    
    
    
}