using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC5___Make_event_public;

[TestFixture]
public class MakePublicHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Visibility can be set to Public, while status is Draft, Active, Ready
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Active)]
    [TestCase(EventStatus.Ready)]
    public async Task Create_Command_To_Make_Event_Public(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithStatus(status)
            .Build();

        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakePublicCommand.Create(id.ToString());
        var handler = new MakePublicHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedTask = Repo.GetByIdAsync(@event.Id);
        var updated = await updatedTask;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updated.Visibility, Is.EqualTo(EventVisibility.Public));
        });
        
    }
    
    // # F1  - Visibility can't be set to Public, while status is Cancelled (Doesn't fail, since we want you to be able to make the command.)
    [Test]
    public async Task Create_Command_To_Make_Cancelled_Event_Public()
    {
        // Arrange
        const EventStatus expected = EventStatus.Cancelled;
        var @event = EventFactory.Create()
            .WithStatus(expected)
            .Build();
        
        await Repo.AddAsync(@event);
        Guid id = @event.Id;
        
        var command = MakePublicCommand.Create(id.ToString());
        var handler = new MakePublicHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedTask = Repo.GetByIdAsync(@event.Id);
        var updated = await updatedTask;
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updated.Visibility, Is.EqualTo(EventVisibility.Private));
        });
    }
}