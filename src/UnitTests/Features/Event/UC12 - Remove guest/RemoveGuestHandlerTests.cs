using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Common.Factories;
using Tests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC12___Remove_guest;

public class RemoveGuestHandlerTests
{
    private FakeEventRepository EventRepository { get; } = new();
    private FakeUserRepository UserRepository { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1
    [Test]
    public async Task Create_RemoveGuestHandler_Success()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var user = UserRepository.Users.First();
        @event.AddGuest(user);    
        await EventRepository.AddAsync(@event);

        var command = RemoveGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new RemoveGuestHandler(EventRepository, UserRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent!.Participants.Any(u => u.Id.Value == user.Id.Value), Is.False);
        });
    }
    
    // # F1
    [Test]
    public async Task Create_RemoveGuestHandler_Fail()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithGuestAndStartTimeInPast();
            
        await EventRepository.AddAsync(@event);
        var user = @event.Participants.First();
        await UserRepository.AddAsync(user);

        var command = RemoveGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new RemoveGuestHandler(EventRepository, UserRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);

        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updatedEvent!.Participants.Any(u => u.Id.Value == user.Id.Value), Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.CancelParticipationToEventInThePast), Is.True);
        });
    }
}