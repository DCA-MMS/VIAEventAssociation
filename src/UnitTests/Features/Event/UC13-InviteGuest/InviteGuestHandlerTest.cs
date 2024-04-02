using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC13_InviteGuest;

public class InviteGuestHandlerTest
{
    private FakeEventRepository EventRepository { get; } = new();
    private FakeUserRepository UserRepository { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1
    [Test]
    [TestCase(EventStatus.Ready)]   
    [TestCase(EventStatus.Active)]
    public async Task Create_InviteGuestHandler_Success(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(status).Build();
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = InviteGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new InviteGuestHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedEvent.Invitations.Any(x => x.GuestId.Value == user.Id.Value));
        });
    }
    
    // # F1
    [Test]
    [TestCase(EventStatus.Draft)]   
    [TestCase(EventStatus.Cancelled)]
    public async Task Create_InviteGuestHandler_Fail(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(status).Build();
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = InviteGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new InviteGuestHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(updatedEvent.Invitations.Any(x => x.GuestId.Value == user.Id.Value), Is.False);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationToNonReadyOrActiveEvent), Is.True);

        });
    }
    
}