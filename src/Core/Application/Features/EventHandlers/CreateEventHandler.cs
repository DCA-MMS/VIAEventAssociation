using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.Features.EventHandlers;

internal class CreateEventHandler(IEventRepository repository, IUnitOfWork uow) : ICommandHandler<CreateEventCommand>
{
    public async Task<Result> HandleAsync(CreateEventCommand command)
    {
        var @event = Event.Create();
        
        await repository.AddAsync(@event);
        await uow.SaveChangesAsync();
        
        command.Id = @event.Id;
        
        return Result.Success();
    }
}