using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC7___Change_capacity;

[TestFixture]
public class ChangeCapacityHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Command to change capacity with values 10, 15, 20 can be handled
    [Test]
    [TestCase(10)]
    [TestCase(15)]
    [TestCase(20)]
    public async Task Handle_Command_To_Change_Event_Capacity(int capacity)
    {
        // Arrange
        var @event = EventFactory.Create()
            .Build();

        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = ChangeCapacityCommand.Create(id.ToString(), capacity);
        var handler = new ChangeCapacityHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That((int) updatedEvent!.Capacity, Is.EqualTo(capacity));
        });
        
    }
    
    // # F1  - Command to change capacity on a cancelled event cannot be handled
    [Test]
    public async Task Handle_Command_To_Change_Cancelled_Event_Capacity_Fails()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Cancelled)
            .WithCapacity(10)
            .Build();
        
        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = ChangeCapacityCommand.Create(id.ToString(), 25);
        var handler = new ChangeCapacityHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That((int) updatedEvent!.Capacity, Is.EqualTo(10)); // Should remain 10
        });
    }
}