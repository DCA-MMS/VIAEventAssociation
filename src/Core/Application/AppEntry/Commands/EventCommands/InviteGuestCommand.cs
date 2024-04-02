using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.EventCommands;

public class InviteGuestCommand
{
    public EventId EventId { get; }
    public UserId UserId { get; }

    private InviteGuestCommand(EventId eventId, UserId userId)
    {
        EventId = eventId;
        UserId = userId;
    }

    public static Result<InviteGuestCommand> Create(string eventIdString, string userIdString)
    {
        var eventId = EventId.FromString(eventIdString);
        var userId = UserId.FromString(userIdString);

        List<Error> errors = [];
        if (eventId.IsFailure || userId.IsFailure)
        {
            errors.AddRange(eventId.Errors);
            errors.AddRange(userId.Errors);
            return Result<InviteGuestCommand>.Failure(errors.ToArray());
        }
        return Result<InviteGuestCommand>.Success(new InviteGuestCommand(eventId, userId));
    }   
}