using Tests.Common.Factories;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace Tests.Features.Event;

public class Usecase12
{
    // # S1
    [Test]
    public void When_Guest_Cancels_Event_They_Are_Participating_Then_Remove_Guest()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var userId = new UserId();

        // Act
        @event.AddGuest(userId);
        var result = @event.RemoveGuest(userId);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Participants, Does.Not.Contain(userId));
        });
    }
    
    // # S2
    [Test]
    public void When_Guest_Cancels_Event_They_Are_Not_Participating_Then_Do_Nothing()
    {
        // Arrange
        var @event = EventTestDataFactory.ActivePublicEvent();
        var userId = new UserId();

        // Act
        var result = @event.RemoveGuest(userId);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
    }

    // # F1
    //TODO
}