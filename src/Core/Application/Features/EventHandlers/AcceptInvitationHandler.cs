using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

public class AcceptInvitationHandler : ICommandHandler<AcceptInvitationCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    internal AcceptInvitationHandler(IEventRepository eventRepository, IUserRepository userRepository, IUnitOfWork uow)
    {
        _eventRepository = eventRepository;
        _userRepository = userRepository;
        _unitOfWork = uow;
    }

    public async Task<Result> HandleAsync(AcceptInvitationCommand command)
    {
        var @event = await _eventRepository.GetByIdAsync(command.EventId);
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (@event is null || user is null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }

        var result = @event.AcceptInvitation(user);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}