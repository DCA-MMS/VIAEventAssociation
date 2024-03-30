using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

internal class MakePublicHandler : ICommandHandler<MakePublicCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    internal MakePublicHandler(IEventRepository repository, IUnitOfWork uow) => (this._repository, this._unitOfWork) = (repository, uow);
    
    public async Task<Result> HandleAsync(MakePublicCommand command)
    {
        var @event = await _repository.GetByIdAsync(command.Id);
        
        if (@event == null)
        {
            return Result.Failure(RepositoryError.ItemNotFound());
        }
        
        var result = @event.MakePublic();
        
        if (result.IsFailure)
        {
            return result;
        }
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}