using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class ChangeDescriptionHandler(IEventRepository repository, IUnitOfWork uow)
    : ICommandHandler<ChangeDescriptionCommand>
{
    public async Task<Result> HandleAsync(ChangeDescriptionCommand command)
    {
        var @event = await repository.GetByIdAsync(command.Id);
        
        if (@event is null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }
        
        var result = @event.ChangeDescription(command.Description);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await uow.SaveChangesAsync();
        return Result.Success();
    }
}