using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class ChangeTitleCommand
{
    EventId Id { get; }
    EventTitle Title { get; }

    private ChangeTitleCommand(EventId id, EventTitle title)
    {
        Id = id;
        Title = title;
    }

    public static Result<ChangeTitleCommand> Create(string id, string title)
    {
        var eventId = EventId.FromString(id);
        var eventTitle = EventTitle.Create(title);

        List<Error> errors = [];
        if (eventId.IsFailure || eventTitle.IsFailure)
        {
            errors.AddRange(eventId.Errors);
            errors.AddRange(eventTitle.Errors);
            return Result<ChangeTitleCommand>.Failure(errors.ToArray());
        }
        return Result<ChangeTitleCommand>.Success(new ChangeTitleCommand(eventId, eventTitle));
    }
}