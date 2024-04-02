using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.Features.EventHandlers;

public class MakePublicHandler : ICommandHandler<MakePublicCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    // # Constructor
    internal MakePublicHandler(IEventRepository repository, IUnitOfWork uow) => (_repository, _unitOfWork) = (repository, uow);
    
    public async Task<Result> HandleAsync(MakePublicCommand command)
    {
        var @event = await _repository.GetByIdAsync(command.Id);
        
        if (@event is null)
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