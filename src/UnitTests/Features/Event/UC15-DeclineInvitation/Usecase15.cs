using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC15_DeclineInvitation;

public class Usecase15
{
    // # S1
    [Test]
    public void When_Guest_Declines_Pending_Invitation_For_Active_Event_Then_Decline_Invitation()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        
        // Act
        var result = @event.DeclineInvitation(invitation.GuestId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(invitation.Status == InvitationStatus.Rejected, Is.True);
        });
    }
    
    // # S2
    [Test]
    public void When_Guest_Declines_Accepted_Invitation_For_Active_Event_Then_Decline_Invitation()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithPendingInvitation();
        var invitation = @event.Invitations.First();
        invitation.Accept();
        
        // Act
        var result = @event.DeclineInvitation(invitation.GuestId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(invitation.Status == InvitationStatus.Rejected, Is.True);
        });
    }
    
    // # F1
    [Test]
    public void When_Guest_Declines_Not_Existing_Invitation_For_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        
        // Act
        var result = @event.DeclineInvitation(new UserId());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationDeclineToGuestNotInvited), Is.True);
        });
    }
    
    // # F2
    [Test]
    public void When_Guest_Declines_Invitation_For_Cancelled_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.CancelledPublicEventWithPendingInvitation();
        
        // Act
        var result = @event.DeclineInvitation(new UserId());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationDeclineToCancelledEvent), Is.True);
        });
    }
    
    // # F3
    [Test]
    public void When_Guest_Declines_Invitation_For_Ready_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.ReadyPublicEventWithPendingInvitation();
        
        // Act
        var result = @event.DeclineInvitation(new UserId());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationDeclineToReadyEvent), Is.True);
        });
    }
}