using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Common.Factories;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC14_AcceptInvitation;

public class AcceptInvitationHandlerTests
{
    private FakeEventRepository EventRepository { get; } = new();
    private FakeUserRepository UserRepository { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1
    [Test]
    public async Task Create_AcceptInvitationHandler_Success()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithPendingInvitation();
        await EventRepository.AddAsync(@event);
        var invitation = @event.Invitations.First();

        var command = AcceptInvitationCommand.Create(@event.Id.Value.ToString(), invitation.GuestId.Value.ToString());
        var handler = new AcceptInvitationHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            var updatedInvitation = updatedEvent.Invitations.FirstOrDefault(x => x.Id == invitation.Id);
            
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedInvitation.Status == InvitationStatus.Accepted, Is.True);
        });
    }
    
    // # F1
    [Test]
    public async Task Create_AcceptInvitationHandler_Fail()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = AcceptInvitationCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new AcceptInvitationHandler(EventRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationAcceptToGuestNotInvited), Is.True);

        });
    }
}