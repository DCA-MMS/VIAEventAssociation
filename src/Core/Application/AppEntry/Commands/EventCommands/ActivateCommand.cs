using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class ActivateCommand
{
    public EventId Id { get; }
    
    private ActivateCommand(EventId id)
    {
        Id = id;
    }
    
    public static Result<ActivateCommand> Create(string id)
    {
        var eventIdResult = EventId.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<ActivateCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<ActivateCommand>.Success(new ActivateCommand(eventIdResult));
    }
}