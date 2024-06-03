using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class MakeReadyHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<MakeReadyCommand>
{
    public async Task<Result> HandleAsync(MakeReadyCommand command)
    {
        var @event = await repository.GetByIdAsync(command.Id);
        
        if (@event == null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }
        
        var result = @event.MakeReady();
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await uow.SaveChangesAsync();
        return Result.Success();
    }
}