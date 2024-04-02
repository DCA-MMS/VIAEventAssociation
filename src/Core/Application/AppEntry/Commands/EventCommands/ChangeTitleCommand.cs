using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class ChangeTitleCommand
{
    public EventId Id { get; }
    public EventTitle Title { get; }

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
        if (!eventId.IsFailure && !eventTitle.IsFailure)
            return Result<ChangeTitleCommand>.Success(new ChangeTitleCommand(eventId, eventTitle));
        errors.AddRange(eventId.Errors);
        errors.AddRange(eventTitle.Errors);
        return Result<ChangeTitleCommand>.Failure(errors.ToArray());
    }
}