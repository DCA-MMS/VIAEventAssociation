using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC12___Remove_guest;

public class RemoveGuestAggregateTests
{
    // # S1
    [Test]
    public void When_Guest_Cancels_Event_They_Are_Participating_Then_Remove_Guest()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        @event.AddGuest(user);
        var result = @event.RemoveGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Participants, Does.Not.Contain(user));
        });
    }
    
    // # S2
    [Test]
    public void When_Guest_Cancels_Event_They_Are_Not_Participating_Then_Do_Nothing()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.RemoveGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
    }

    // # F1
    [Test]
    public void When_Guest_Cancels_Event_From_The_Past_Then_Request_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithGuestAndStartTimeInPast();
        var user = @event.Participants.First();

        // Act
        var result = @event.RemoveGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.CancelParticipationToEventInThePast), Is.True);
        });
    }}