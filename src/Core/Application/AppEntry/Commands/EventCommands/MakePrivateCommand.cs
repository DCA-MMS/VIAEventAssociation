using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class MakePrivateCommand
{
    public EventId Id { get; }
    
    private MakePrivateCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<MakePrivateCommand> Create(string id)
    {
        var eventIdResult = EventId.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<MakePrivateCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<MakePrivateCommand>.Success(new MakePrivateCommand(eventIdResult));
    }
}