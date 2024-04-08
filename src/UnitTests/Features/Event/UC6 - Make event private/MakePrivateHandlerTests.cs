using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC6___Make_event_private;

[TestFixture]
public class MakePrivateHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Visibility can be set to Private, while status is Draft, Ready
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Ready)]
    public async Task Handle_Command_To_Make_Event_Private(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(status)
            .Build();

        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakePrivateCommand.Create(id.ToString());
        var handler = new MakePrivateHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent!.Visibility, Is.EqualTo(EventVisibility.Private));
        });
        
    }
    
    // # F1  - Visibility can't be set to Private, while status is Cancelled
    [Test]
    public async Task Handle_Command_To_Make_Cancelled_Event_Private()
    {
        // Arrange
        const EventStatus expected = EventStatus.Cancelled;
        var @event = EventFactory.Create()
            .WithStatus(expected)
            .WithVisibility(EventVisibility.Public)
            .Build();
        
        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakePrivateCommand.Create(id.ToString());
        var handler = new MakePrivateHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedEvent = await Repo.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updatedEvent!.Visibility, Is.EqualTo(EventVisibility.Public));
        });
    }
}