using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.AppEntry.Commands.EventCommands;

internal class MakePrivateCommand
{
    public Id<Event> Id { get; }
    
    private MakePrivateCommand(Id<Event> id)
    {
        Id = id;
    }
    
    public static Result<MakePrivateCommand> Create(string id)
    {
        var eventIdResult = Id<Event>.FromString(id);
        
        if (eventIdResult.IsFailure)
        {
            return Result<MakePrivateCommand>.Failure(eventIdResult.Errors.ToArray());
        }
        
        return Result<MakePrivateCommand>.Success(new MakePrivateCommand(eventIdResult));
    }
}