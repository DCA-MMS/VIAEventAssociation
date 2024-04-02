using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakePublicCommand
{
    public EventId Id { get; }
    
    // # Constructor
    private MakePublicCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<MakePublicCommand> Create(string id)
    {
        // - Convert the string id to Id<Event>
        var eventId = EventId.FromString(id);
        
        // ? If the conversion is successful, return a success result with the MakePublicCommand object
        if (!eventId.IsFailure)
            return Result<MakePublicCommand>.Success(new MakePublicCommand(eventId));
        
        // ! If the conversion failed, return a failure result with the errors
        return Result<MakePublicCommand>.Failure(eventId.Errors.ToArray());
    }
    
}