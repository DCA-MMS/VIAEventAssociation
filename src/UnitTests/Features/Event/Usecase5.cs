using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase5
{
    
    // # S1 - Visibility can be set to Public, while status is Draft, Active, Ready
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Active)]
    [TestCase(EventStatus.Ready)]
    public void VisibilityCanBeSetToPublicWhileStatusIsDraftActiveReady(EventStatus status)
    {
        // Arrange
        var expected = status;
        var @event = EventFactory.Create()
            .WithStatus(expected)
            .Build();
        
        // Act
        var result = @event.MakePublic();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Visibility, Is.EqualTo(EventVisibility.Public));
        });
        
    }
    
    // # F1  - Visibility can't be set to Public, while status is Cancelled
    [Test]
    public void VisibilityCantBeSetToPublicWhileStatusIsCancelled()
    {
        // Arrange
        const EventStatus expected = EventStatus.Cancelled;
        var @event = EventFactory.Create()
            .WithStatus(expected)
            .Build();
        
        // Act
        var result = @event.MakePublic();        
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventVisibilityError.NotModifiable().Code));
        });
    }
}