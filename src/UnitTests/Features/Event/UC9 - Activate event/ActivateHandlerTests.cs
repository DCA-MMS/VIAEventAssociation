using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC9___Activate_event;

[TestFixture]
public class ActivateHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Command to activate event with valid data can be handled
    [Test]
    public async Task Handle_Command_To_Activate_Event()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .Build();

        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = ActivateCommand.Create(id.ToString());
        var handler = new ActivateHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent!.Status, Is.EqualTo(EventStatus.Active));
        });
    }
    
    // # F1  - Command to activate event with status Cancelled can't be handled
    [Test]
    public async Task Handle_Command_To_Activate_Cancelled_Event()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = ActivateCommand.Create(id.ToString());
        var handler = new ActivateHandler(Repo, Uow);
        
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