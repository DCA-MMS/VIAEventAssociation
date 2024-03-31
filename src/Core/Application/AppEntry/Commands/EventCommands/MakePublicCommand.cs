using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakePublicCommand
{
    public Id<Event> Id { get; }
    
    // # Constructor
    private MakePublicCommand(Id<Event> id)
    {
        Id = id;
    }
    
    public static Result<MakePublicCommand> Create(string id)
    {
        // - Convert the string id to Id<Event>
        var eventId = Id<Event>.FromString(id);
        
        // ? If the conversion is successful, return a success result with the MakePublicCommand object
        if (!eventId.IsFailure)
            return Result<MakePublicCommand>.Success(new MakePublicCommand(eventId));
        
        // ! If the conversion failed, return a failure result with the errors
        return Result<MakePublicCommand>.Failure(eventId.Errors.ToArray());
    }
    
}