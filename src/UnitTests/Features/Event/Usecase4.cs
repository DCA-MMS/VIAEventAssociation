using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase4
{
    // # S1 - Time change
    [Test]
    public void Given_An_Event_Changing_The_TimeRange_Into_Another_Time_Then_The_TimeRange_Should_Be_Updated()
    {
        var expectedStart = DateTime.Today.AddHours(8);
        var expectedEnd = DateTime.Today.AddHours(10);
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            var timeRange = @event.Value.TimeRange;
            
            // Assert
            Assert.That(timeRange.Start, Is.EqualTo(expectedStart));
            Assert.That(timeRange.End, Is.EqualTo(expectedEnd));
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S2 - Date change
    [Test]
    public void Given_An_Event_Changing_The_Date_Into_Another_Date_Then_The_Date_Should_Be_Updated()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddDays(1).AddHours(8);
        var expectedEnd = DateTime.Today.AddDays(1).AddHours(10);
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            var timeRange = @event.Value.TimeRange;
            
            // Assert
            Assert.That(timeRange.Start, Is.EqualTo(expectedStart));
            Assert.That(timeRange.End, Is.EqualTo(expectedEnd));
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S3 - Status change from ready to draft
    [Test]
    public void Given_An_Event_Changing_The_TimeRange_Into_Another_Time_Then_The_Status_Should_Be_Draft()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(8);
        var expectedEnd = DateTime.Today.AddHours(10);
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
            .Build();
        
        // Act
        @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            var status = @event.Value.Status;
            
            // Assert
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S4 - Date and time in the future
    [Test]
    public void Given_An_Event_Changing_The_TimeRange_Into_Another_Time_Then_The_TimeRange_Should_Be_In_The_Future()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddDays(1).AddHours(8);
        var expectedEnd = DateTime.Today.AddDays(1).AddHours(10);
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            var timeRange = @event.Value.TimeRange;
            
            // Assert
            Assert.That(timeRange.Start, Is.EqualTo(expectedStart));
            Assert.That(timeRange.End, Is.EqualTo(expectedEnd));
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S5 - Different durations
    [Test]
    [TestCase(8, 9)]
    [TestCase(11, 13)]
    [TestCase(13, 17)]
    [TestCase(8, 18)]
    public void Given_An_Event_Changing_The_TimeRange_Into_Another_Time_Then_The_TimeRange_Should_Be_Updated_With_The_New_Duration(int startHour, int endHour)
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(startHour);
        var expectedEnd = DateTime.Today.AddHours(endHour);
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            var timeRange = @event.Value.TimeRange;
            
            // Assert
            Assert.That(timeRange.Start, Is.EqualTo(expectedStart));
            Assert.That(timeRange.End, Is.EqualTo(expectedEnd));
            Assert.That(@event.Value.Status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # F1 - Start date after end date
    [Test]
    public void Given_Start_Date_After_End_Date_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddDays(1);
        var expectedEnd = DateTime.Today;
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartAfterEndDate().Code));
        });
    }
    
    // # F2 - Start time after end time
    [Test]
    public void Given_Start_Time_After_End_Time_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(10);
        var expectedEnd = DateTime.Today.AddHours(8);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartAfterEndTime().Code));
        });
    }
    
    // # F3 - Duration less than 1 hour
    [Test]
    public void Given_Duration_Less_Than_1_Hour_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(10);
        var expectedEnd = DateTime.Today.AddHours(10).AddMinutes(30);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.DurationLessThanOneHour().Code));
        });
    }
    
    // # F4 - Start before 08:00
    [Test]
    public void Given_Start_Before_08_00_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(7);
        var expectedEnd = DateTime.Today.AddHours(10);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartBeforeEight().Code));
        });
    }
    
    // # F5 - Start before 01:00 and end after 01:00
    [Test]
    public void Given_Start_Before_01_00_And_End_After_01_00_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddMinutes(55);
        var expectedEnd = DateTime.Today.AddDays(1).AddHours(1);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartBeforeEight().Code));
        });
    }
    
    // # F6 - Status is active
    [Test]
    public void Given_Status_Is_Active_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(8);
        var expectedEnd = DateTime.Today.AddHours(10);
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.NotModifiable().Code));
        });
    }
    
    // # F7 - Status is cancelled
    [Test]
    public void Given_Status_Is_Cancelled_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(8);
        var expectedEnd = DateTime.Today.AddHours(10);
        var @event = EventFactory.Create()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.NotModifiable().Code));
        });
    }
    
    // # F8 - Duration longer than 10 hours
    [Test]
    public void Given_Duration_Longer_Than_10_Hours_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddHours(8);
        var expectedEnd = DateTime.Today.AddHours(20);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.DurationIsLongerThanTenHours().Code));
        });
    }
    
    // # F9 - Start date in the past
    [Test]
    public void Given_Start_Date_In_The_Past_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddDays(-1).AddHours(8);
        var expectedEnd = DateTime.Today.AddDays(-1).AddHours(10);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartIsInPast().Code));
        });
    }
    
    // # F10 - Start is before 01:00 on same date as end and end is after 08:00
    [Test]
    public void Given_Start_Before_01_00_And_End_After_08_00_On_Same_Date_Should_Return_Error()
    {
        // Arrange
        var expectedStart = DateTime.Today.AddMinutes(55);
        var expectedEnd = DateTime.Today.AddHours(8).AddMinutes(30);
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.Value.ChangeTimeRange(expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartBeforeEight().Code));
        });
    }
    
}