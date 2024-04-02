using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakeReadyCommand
{
    public EventId Id { get; }
    
    private MakeReadyCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<MakeReadyCommand> Create(string id)
    {
        var eventIdResult = EventId.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<MakeReadyCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<MakeReadyCommand>.Success(new MakeReadyCommand(eventIdResult));
    }
}