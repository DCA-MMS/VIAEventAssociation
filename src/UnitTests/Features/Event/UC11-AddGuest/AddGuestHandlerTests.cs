using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Common.Factories;
using Tests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC11_AddGuest;

public class AddGuestHandlerTests
{
    private FakeEventRepository EventRepository { get; } = new();
    private FakeUserRepository UserRepository { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1
    [Test]
    public async Task Create_AddGuestHandler_Success()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = AddGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new AddGuestHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent.Participants.Any(id => id.Value == user.Id.Value), Is.True);
        });
    }
    
    // # F1
    [Test]
    public async Task Create_AddGuestHandler_Fail()
    {
        // Arrange
        var @event = EventTestDataFactory.PublicEvent();
            
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = AddGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new AddGuestHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);

        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updatedEvent.Participants.Any(id => id.Value == user.Id.Value), Is.False);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventThatIsNotActive), Is.True);

        });
    }
}