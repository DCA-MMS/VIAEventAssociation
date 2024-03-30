using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakePublicCommand
{
    public EventId Id { get; }
    
    private MakePublicCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<MakePublicCommand> Create(string id)
    {
        var eventIdResult = EventId.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<MakePublicCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<MakePublicCommand>.Success(new MakePublicCommand(eventIdResult));
    }
}