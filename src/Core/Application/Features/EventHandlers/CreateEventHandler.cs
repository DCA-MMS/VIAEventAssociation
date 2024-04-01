using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.Features.EventHandlers;

public class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    // # Constructor
    internal CreateEventHandler(IEventRepository repository, IUnitOfWork uow) => (_repository, _unitOfWork) = (repository, uow);
    
    public async Task<Result> HandleAsync(CreateEventCommand command)
    {
        var @event = EventFactory.Create()
            .Build();
        
        await _repository.AddAsync(@event);
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Success();
    }
}