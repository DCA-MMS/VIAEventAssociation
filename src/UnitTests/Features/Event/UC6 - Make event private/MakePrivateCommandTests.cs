using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC6___Make_event_private;

[TestFixture]
public class MakePrivateCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();
    
    // # S1 - Visibility can be set to Private, while status is Draft, Active, Ready
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Active)]
    [TestCase(EventStatus.Ready)]
    public void Create_Command_To_Make_Event_Private(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(status)
            .Build();

        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = MakePrivateCommand.Create(id.ToString());
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
        
    }
    
    // # F1  - Visibility can't be set to Private, while status is Cancelled (Doesn't fail, since we want you to be able to make the command.)
    [Test]
    public void Create_Command_To_Make_Cancelled_Event_Private()
    {
        // Arrange
        const EventStatus expected = EventStatus.Cancelled;
        var @event = EventFactory.Create()
            .WithStatus(expected)
            .Build();
        
        EventRepo.AddAsync(@event);
        Guid id = @event.Id;
        
        // Act
        var result = MakePrivateCommand.Create(id.ToString());      
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
    }
}