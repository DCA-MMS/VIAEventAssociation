using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

public class RemoveGuestHandler  : ICommandHandler<RemoveGuestCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    internal RemoveGuestHandler(IEventRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _unitOfWork = uow;
    }

    public async Task<Result> HandleAsync(RemoveGuestCommand command)
    {
        var @event = await _repository.GetByIdAsync(command.EventId);

        if (@event is null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }

        var result = @event.RemoveGuest(command.UserId);

        if (result.IsFailure)
        {
            return result;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}