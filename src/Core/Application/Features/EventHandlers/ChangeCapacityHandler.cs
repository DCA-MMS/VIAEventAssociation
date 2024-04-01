using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

public class ChangeCapacityHandler : ICommandHandler<ChangeCapacityCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    internal ChangeCapacityHandler(IEventRepository repository, IUnitOfWork uow) => (_repository, _unitOfWork) = (repository, uow);
    
    public async Task<Result> HandleAsync(ChangeCapacityCommand command)
    {
        var @event = await _repository.GetByIdAsync(command.Id);
        
        if (@event == null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }
        
        var result = @event.ChangeCapacity(command.Capacity);
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}