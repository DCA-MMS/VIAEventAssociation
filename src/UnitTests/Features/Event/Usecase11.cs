using Tests.Common.Factories;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event;

public class Usecase11
{
    // # S1
    [Test]
    public void When_Guest_Attend_Active_Public_Event_Then_Event_Registered_The_Guest()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var userId = new UserId();

        // Act
        var result = @event.AddGuest(userId);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Participants.Any(x => x == userId), Is.True);
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
        var @event = EventTestDataFactory.PublicEvent();
        @event.ChangeStatus(status);
        var userId = new UserId();

        // Act
        var result = @event.AddGuest(userId);

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
        var userId = new UserId();

        // Act
        var result = @event.AddGuest(userId);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToFullEvent), Is.True);
        });
    }
    
    // # F3
    //TODO
    
    // # F4
    [Test]
    public void When_Guest_Attend_Private_Event_Then_Guest_Is_Rejected()
    {
        // Arrange
        var @event = EventTestDataFactory.PrivateEvent();
        var userId = new UserId();

        // Act
        var result = @event.AddGuest(userId);

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
        var userId = new UserId();

        // Act
        @event.AddGuest(userId);
        var result = @event.AddGuest(userId);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.RequestToEventGuestIsAlreadyPartaking), Is.True);
        });
    }
}