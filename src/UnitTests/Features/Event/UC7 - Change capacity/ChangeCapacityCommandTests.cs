using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC7___Change_capacity;

[TestFixture]
public class ChangeCapacityCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();
    
    // # S1 - Command to change capacity with values 10, 15, 20 can be created
    [Test]
    [TestCase(10)]
    [TestCase(15)]
    [TestCase(20)]
    public void Create_Command_To_Change_Event_Capacity(int capacity)
    {
        // Arrange
        var @event = EventFactory.Create()
            .Build();

        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = ChangeCapacityCommand.Create(id.ToString(), capacity);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
        
    }
    
    // # F1  - Command to change capacity with values 1, 3, 51 cannot be created
    [Test]
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(51)]
    public void Create_Command_To_Change_Cancelled_Event_Capacity(int capacity)
    {
        // Arrange
        var @event = EventFactory.Create()
            .Build();
        
        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = ChangeCapacityCommand.Create(id.ToString(), capacity);      
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
        });
    }
}