using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC8___Make_event_ready;

[TestFixture]
public class MakeReadyHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Command to make event ready with valid data can be handled
    [Test]
    public async Task Handle_Command_To_Make_Event_Ready()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .Build();

        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakeReadyCommand.Create(id.ToString());
        var handler = new MakeReadyHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent!.Status, Is.EqualTo(EventStatus.Ready));
        });
    }
    
    // # F1  - Command to make event ready with status Cancelled can't be handled
    [Test]
    public async Task Handle_Command_To_Make_Cancelled_Event_Ready()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakeReadyCommand.Create(id.ToString());
        var handler = new MakeReadyHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updatedEvent!.Status, Is.EqualTo(EventStatus.Cancelled));
        });
    }
}