﻿using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC13___Invite_guest;

public class InviteGuestAggregateTests
{
    
    // # S1
    [Test]
    [TestCase(EventStatus.Ready)]   
    [TestCase(EventStatus.Active)]
    public void When_Guest_Is_Invited_To_Ready_Or_Active_Event_Then_Add_Pending_Invitation_In_Event(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(status).Build();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;
        
        // Act
        var result = @event.InviteGuest(user);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Invitations.Any(x => x.Guest == user));
        });
    }
    
    // # F1
    [Test]
    [TestCase(EventStatus.Draft)]   
    [TestCase(EventStatus.Cancelled)]
    public void When_Guest_Is_Invited_To_Draft_Or_Cancelled_Event_Then_Reject_Request(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(status).Build();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;
        
        // Act
        var result = @event.InviteGuest(user);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationToNonReadyOrActiveEvent), Is.True);
        });
    }
    
    // # F2
    [Test]
    public void When_Guest_Is_Invited_To_Full_Active_Event_Then_Reject_Request()
    {
        // Arrange
        var @event = EventTestDataFactory.FullActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;
        
        // Act
        var result = @event.InviteGuest(user);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.InvitationToFullEvent), Is.True);
        });
    }
}