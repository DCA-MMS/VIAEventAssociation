using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class ChangeCapacityCommand
{
    public EventId Id { get; }
    public EventCapacity Capacity { get; }
    
    private ChangeCapacityCommand(EventId id, EventCapacity capacity)
    {
        Id = id;
        Capacity = capacity;
    }
    
public static Result<ChangeCapacityCommand> Create(string id, int capacity)
    {
        var eventIdResult = EventId.FromString(id);
        var capacityResult = EventCapacity.Create(capacity);
        
        if (eventIdResult.IsFailure || capacityResult.IsFailure)
        {
            List<Error> errors = [];
            errors.AddRange(eventIdResult.Errors);
            errors.AddRange(capacityResult.Errors);
            return Result<ChangeCapacityCommand>.Failure(errors.ToArray());
        }
        
        return Result<ChangeCapacityCommand>.Success(new ChangeCapacityCommand(eventIdResult, capacityResult));
    }
}