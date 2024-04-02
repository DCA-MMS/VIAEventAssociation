using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC14_AcceptInvitation;

public class Usecase14
{
    // # S1
    [Test]
    public void When_Guest_Accept_Pending_Invitation_To_Event_Then_Accept_Invitation()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        
        // Act
        var result = @event.AcceptInvitation(invitation.GuestId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(invitation.Status == InvitationStatus.Accepted, Is.True);
        });
    }
    
    // # F1
    [Test]
    public void When_Guest_Accept_Invitation_To_Event_Guest_Is_Not_Invited_To_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        
        // Act
        var result = @event.AcceptInvitation(new UserId());
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationAcceptToGuestNotInvited), Is.True);
        });
    }
    
    // # F2
    [Test]
    public void When_Guest_Accept_Pending_Invitation_To_Full_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.FullActivePublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        // Act
        var result = @event.AcceptInvitation(invitation.GuestId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationAcceptToGuestNotInvited), Is.True);
        });
    }
    
    // # F3
    [Test]
    public void When_Guest_Accept_Pending_Invitation_To_Cancelled_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.CancelledPublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        // Act
        var result = @event.AcceptInvitation(invitation.GuestId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationAcceptToCancelledEvent), Is.True);
        });
    }
    
    // # F4
    [Test]
    public void When_Guest_Accept_Pending_Invitation_To_Ready_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.ReadyPublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        // Act
        var result = @event.AcceptInvitation(invitation.GuestId);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationAcceptToReadyEvent), Is.True);
        });
    }
    
}