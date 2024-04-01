using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

public class ActivateCommand
{
    public Id<Event> Id { get; }
    
    private ActivateCommand(Id<Event> id)
    {
        Id = id;
    }
    
    public static Result<ActivateCommand> Create(string id)
    {
        var eventIdResult = Id<Event>.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<ActivateCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<ActivateCommand>.Success(new ActivateCommand(eventIdResult));
    }
}