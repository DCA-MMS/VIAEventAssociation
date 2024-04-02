using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class ChangeDurationCommand
{
    public EventId Id { get; }
    public TimeRange Duration { get; }
    
    // # Constructor
    private ChangeDurationCommand(EventId id, TimeRange duration)
    {
        Id = id;
        Duration = duration;
    }
    
    public static Result<ChangeDurationCommand> Create(string id, DateTime start, DateTime end)
    {
        // - Convert the string id to EventId
        var eventId = EventId.FromString(id);
        // - Convert the string start to Time
        var timeRange = TimeRange.Create(start, end);
        
        List<Error> errors = [];
        
        // ? If all conversions are successful, return a success result with the ChangeDurationCommand object
        if (!eventId.IsFailure && !timeRange.IsFailure)
            return Result<ChangeDurationCommand>.Success(new ChangeDurationCommand(eventId,timeRange ));
        
        // ! If one or more conversions failed, return a failure result with the errors
        errors.AddRange(eventId.Errors);
        errors.AddRange(timeRange.Errors);
        return Result<ChangeDurationCommand>.Failure(errors.ToArray());
    }
}