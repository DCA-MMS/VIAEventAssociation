using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakeReadyCommand
{
    public Id<Event> Id { get; }
    
    private MakeReadyCommand(Id<Event> id)
    {
        Id = id;
    }
    
    public static Result<MakeReadyCommand> Create(string id)
    {
        var eventIdResult = Id<Event>.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<MakeReadyCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<MakeReadyCommand>.Success(new MakeReadyCommand(eventIdResult));
    }
}