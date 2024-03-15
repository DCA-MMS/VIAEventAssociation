using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase9
{
    // # S1
    [Test]
    public void Activate_Event_With_Valid_Data_With_Status_Draft_Should_Succeed()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Event Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Active));
        });
    }
    
    // # S2
    [Test]
    public void Activate_Event_With_Status_Ready_Should_Succeed()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Event Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Ready)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Active));
        });
    }
    
    // # S3
    [Test]
    public void Activate_Event_With_Status_Active_Should_Do_Nothing()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Event Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Active)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Active));
        });
    }
    
    // # F1 Invalid title
    [Test]
    public void Activate_Event_With_Default_Title_With_Status_Draft_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Working Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddDays(1).AddHours(8),
                DateTime.Today.AddDays(1).AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateWhenTitleIsDefault().Code));
        });
    }
    
    // # F1 Invalid TimeRange
    [Test]
    public void Activate_Event_With_Invalid_TimeRange_With_Status_Draft_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Event Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddDays(-1).AddHours(8),
                DateTime.Today.AddDays(-1).AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Draft)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateEventWithStartTimePriorToNow().Code));
        });
    }
    
    // # F2
    [Test]
    public void Activate_Event_With_Status_Cancelled_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create()
            .WithTitle("Event Title")
            .WithDescription("Event Description")
            .WithCapacity(40)
            .WithTimeRange(
                DateTime.Today.AddHours(8),
                DateTime.Today.AddHours(12))
            .WithVisibility(EventVisibility.Public)
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        // Act
        var result = @event.Value.Activate();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Cancelled));
            Assert.That(result.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == EventError.CantReadyOrActivateCancelledEvent().Code));
        });
    }
}