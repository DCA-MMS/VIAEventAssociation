using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class ChangeTitleHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<ChangeTitleCommand>
{
    public async Task<Result> HandleAsync(ChangeTitleCommand command)
    {
        var @event = await repository.GetByIdAsync(command.Id);

        if (@event is null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }

        var result = @event.ChangeTitle(command.Title);

        if (result.IsFailure)
        {
            return result;
        }

        await uow.SaveChangesAsync();
        return Result.Success();

    }
}