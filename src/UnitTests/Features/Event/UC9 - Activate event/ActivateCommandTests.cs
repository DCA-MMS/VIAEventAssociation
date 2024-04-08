using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;

namespace Tests.Features.Event.UC9___Activate_event;

[TestFixture]
public class ActivateCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();
    
    // # S1 - Command to activate event with valid id can be created
    [Test]
    public void Create_Command_To_Activate_Event_With_Valid_Id()
    {
        // Arrange
        var @event = EventFactory.Create()
            .Build();

        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = ActivateCommand.Create(id.ToString());
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
        
    }
    
    // # F1  - Command to activate event with invalid id can't be created
    [Test]
    public void Create_Command_To_Activate_Event_With_Invalid_Id()
    {
        // Act
        var result = ActivateCommand.Create("Invalid GUID");      
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
        });
    }
}