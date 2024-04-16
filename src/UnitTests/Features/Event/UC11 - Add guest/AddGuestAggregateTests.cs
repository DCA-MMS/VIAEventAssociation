using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event.UC11___Add_guest;

public class AddGuestAggregateTests
{
    // # S1
    [Test]
    public void When_Guest_Attend_Active_Public_Event_Then_Event_Registered_The_Guest()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Participants.Any(x => x.Id == user.Id), Is.True);
        });
    }
    
    // # F1
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Ready)]   
    [TestCase(EventStatus.Cancelled)]
    public void When_Guest_Attend_Non_Active_Event_Then_Guest_Is_Rejected(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(status).Build();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventThatIsNotActive), Is.True);
        });
    }
    
    // # F2
    [Test]
    public void When_Guest_Attend_Full_Active_Public_Event_Then_Guest_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.FullActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToFullEvent), Is.True);
        });
    }
    
    // # F3
    [Test]
    public void When_Guest_Attend_Event_From_The_Past_Then_Guest_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEventWithStartTimeInPast();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventInThePast), Is.True);
        });
    }    
    
    
    // # F4
    [Test]
    public void When_Guest_Attend_Private_Event_Then_Guest_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.PrivateEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventThatIsNotPublic), Is.True);
        });
    }
    
    // # F5
    [Test]
    public void When_Guest_Attend_Event_Second_Time_Then_Guest_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var user = User.Create(FullName.Create("Bob", "Bobsen"), Email.Create("bob@via.dk")).Value;

        // Act
        @event.AddGuest(user);
        var result = @event.AddGuest(user);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventGuestIsAlreadyPartaking), Is.True);
        });
    }
}