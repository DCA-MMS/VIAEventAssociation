﻿using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Common.Factories;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC15___Decline_invitation;

public class DeclineInvitationHandlerTests
{
    private FakeEventRepository EventRepository { get; } = new();
    private FakeUserRepository UserRepository { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1
    [Test]
    public async Task Create_DeclineInvitationHandler_Success()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithPendingInvitation();
        await EventRepository.AddAsync(@event);
        var invitation = @event.Invitations.First();
        await UserRepository.AddAsync(invitation.Guest);

        var command = DeclineInvitationCommand.Create(@event.Id.Value.ToString(), invitation.Guest.Id.Value.ToString());
        var handler = new DeclineInvitationHandler(EventRepository, UserRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        var updatedEvent = await EventRepository.GetByIdAsync(@event.Id);
        
        // Assert
        Assert.Multiple(() =>
        {
            var updatedInvitation = updatedEvent!.Invitations.FirstOrDefault(x => x.Id == invitation.Id);
            
            Assert.That(result.IsFailure, Is.False);
            Assert.That(updatedInvitation!.Status, Is.EqualTo(InvitationStatus.Declined));
        });
    }
    
    // # F1
    [Test]
    public async Task Create_DeclineInvitationHandler_Fail()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        await EventRepository.AddAsync(@event);
        var user = UserRepository.Users.First();

        var command = DeclineInvitationCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
        var handler = new DeclineInvitationHandler(EventRepository, UserRepository, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationDeclineToGuestNotInvited), Is.True);

        });
    }
}