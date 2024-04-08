using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC8___Make_event_ready;

public class MakeReadyCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();
    
    // # S1 - Command to make event ready with valid id can be created
    [Test]
    public void Create_Command_To_Make_Event_Ready_With_Valid_Id()
    {
        // Arrange
        var @event = EventFactory.Create()
            .Build();

        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = MakeReadyCommand.Create(id.ToString());
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
        
    }
    
    // # F1  - Command to make event ready with invalid id can't be created
    [Test]
    public void Create_Command_To_Make_Event_Ready_With_Invalid_Id()
    {
        // Act
        var result = MakeReadyCommand.Create("Invalid GUID");      
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
        });
    }
}