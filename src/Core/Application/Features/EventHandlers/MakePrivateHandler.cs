using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class MakePrivateHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<MakePrivateCommand>
{
    public async Task<Result> HandleAsync(MakePrivateCommand command)
    {
        var @event = await repository.GetByIdAsync(command.Id);
        
        if (@event == null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }
        
        var result = @event.MakePrivate();
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await uow.SaveChangesAsync();
        return Result.Success();
    }
}