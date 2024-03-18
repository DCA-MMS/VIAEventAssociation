using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase6
{
    
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Ready)]
    public void Changing_Event_From_Private_To_Private_With_Status_Draft_Or_Ready_Should_Make_Event_Private(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithVisibility(EventVisibility.Private).WithStatus(status).Build();
        
        // Act
        var result = @event.MakePrivate();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Visibility, Is.EqualTo(EventVisibility.Private));
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    [Test]
    [TestCase(EventStatus.Draft)]
    [TestCase(EventStatus.Ready)]
    public void Changing_Event_From_Public_To_Private_With_Status_Draft_Or_Ready_Should_Make_Event_Private(EventStatus status)
    {
        // Arrange
        var @event = EventFactory.Create().WithVisibility(EventVisibility.Public).WithStatus(status).Build();
        
        // Act
        var result = @event.MakePrivate();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Visibility, Is.EqualTo(EventVisibility.Private));
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    [Test]
    [TestCase(EventVisibility.Private)]
    [TestCase(EventVisibility.Public)]
    public void Changing_Event_To_Private_With_Active_Status_Should_Fail(EventVisibility visibility)
    {
        // Arrange
        var @event = EventFactory.Create().WithVisibility(visibility).WithStatus(EventStatus.Active).Build();
        
        // Act
        var result = @event.MakePrivate();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventVisibilityError.NotModifiable().Code));
        });
    }
    
    [Test]
    [TestCase(EventVisibility.Private)]
    [TestCase(EventVisibility.Public)]
    public void Changing_Event_To_Private_With_Cancelled_Status_Should_Fail(EventVisibility visibility)
    {
        // Arrange
        var @event = EventFactory.Create().WithVisibility(visibility).WithStatus(EventStatus.Cancelled).Build();
        
        // Act
        var result = @event.MakePrivate();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventVisibilityError.NotModifiable().Code));
        });
    }
}