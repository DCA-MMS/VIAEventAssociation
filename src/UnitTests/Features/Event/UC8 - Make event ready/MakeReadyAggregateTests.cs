using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event.UC8___Make_event_ready;

public class MakeReadyAggregateTests
{
    // # S1
    [Test]
    public void Ready_Event_With_Status_Draft_With_Valid_Data_Should_Succeed()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .WithDescription("A valid description")
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithCapacity(20)
            .Build();
        
        // Act
        var result = @event.MakeReady();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Ready));
        });
    }
    
    // TODO: Not sure this test makes sense? The other methods that sets the data should have already validated the data?
    // # F1
    /*
    [Test]
    public void Ready_Event_With_Status_Draft_With_Invalid_Data_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .WithDescription("A valid description")
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .WithTimeRange(
                new DateTime(2027, 3, 11, 12, 0, 0),
                new DateTime(2027, 3, 11, 18, 0, 0))
            .WithCapacity(20)
            .Build();
        
        // Act
        var result = @event.MakeReady();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    */
    
    // # F2
    [Test]
    public void Ready_Event_With_Status_Cancelled_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .WithDescription("A valid description")
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Cancelled)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithCapacity(20)
            .Build();
        
        // Act
        var result = @event.MakeReady();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Cancelled));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateCancelledEvent().Code));
        });
    }
    
    // # F3
    [Test]
    public void Ready_Event_With_Start_Time_Prior_To_Time_Of_Readying_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("A valid title")
            .WithDescription("A valid description")
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .WithTimeRange(
                DateTime.Today.AddDays(-1).AddHours(8),
                DateTime.Today.AddDays(-1).AddHours(12))
            .WithCapacity(20)
            .Build();
        
        // Act
        var result = @event.MakeReady();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Draft));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateEventWithStartTimePriorToNow().Code));
        });
    }
    
    // # F4
    [Test]
    public void Ready_Event_With_Default_Title_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithDescription("A valid description")
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithCapacity(20)
            .Build();
        
        // Act
        var result = @event.MakeReady();
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Status, Is.EqualTo(EventStatus.Draft));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateWhenTitleIsDefault().Code));
        });
    }
}