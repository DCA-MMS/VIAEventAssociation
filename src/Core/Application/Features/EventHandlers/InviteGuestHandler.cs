using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class InviteGuestHandler(IEventRepository eventRepository, IUserRepository userRepository, IUnitOfWork uow)
    : ICommandHandler<InviteGuestCommand>
{
    public async Task<Result> HandleAsync(InviteGuestCommand command)
    {
        var @event = await eventRepository.GetByIdAsync(command.EventId);
        var user = await userRepository.GetByIdAsync(command.UserId);

        if (@event is null || user is null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }

        var result = @event.InviteGuest(user);

        if (result.IsFailure)
        {
            return result;
        }

        await uow.SaveChangesAsync();
        return Result.Success();
    }
}