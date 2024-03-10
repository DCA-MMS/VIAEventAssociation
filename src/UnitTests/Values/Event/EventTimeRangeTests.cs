using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Values.Event;

[TestFixture]
public class EventTimeRangeTests
{
    // # LIMIT (1 HOUR)
    [Test]
    public void Create_WithValidValues_ShouldReturnSuccessResult_1()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(10);
        var end = DateTime.Today.AddDays(1).AddHours(11);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # VALID (5 HOURS)
    [Test]
    public void Create_WithValidValues_ShouldReturnSuccessResult_5()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(8);
        var end = DateTime.Today.AddDays(1).AddHours(13);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # LIMIT (10 HOURS)
    [Test]
    public void Create_WithValidValues_ShouldReturnSuccessResult_10()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(8);
        var end = DateTime.Today.AddDays(1).AddHours(18);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    
    // # START IN PAST (INVALID)
    [Test]
    public void Create_WithStartInPast_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(-1);
        var end = DateTime.Today.AddHours(1);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartIsInPast().Code));
        });
    }

    // # START DATE AFTER END DATE (INVALID)
    [Test]
    public void Create_WithStartAfterEnd_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1);
        var end = DateTime.Today.AddDays(1).AddHours(-1);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartAfterEndDate().Code));
        });
    }

    // # START BEFORE 08:00 (INVALID)
    [Test]
    public void Create_WithStartBeforeEight_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(7);
        var end = DateTime.Today.AddDays(1).AddHours(8);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartBeforeEight().Code));
        });
    }

    // # START TIME AFTER END TIME (INVALID)
    [Test]
    public void Create_WithStartAfterEndTime_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(9);
        var end = DateTime.Today.AddDays(1).AddHours(8);

        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert (That it has one error)
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.StartAfterEndTime().Code));
        });
    }
    
    // # DURATION LESS THAN 1 HOUR (INVALID)
    [Test]
    public void Create_WithDurationLessThanOneHour_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(8);
        var end = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(59);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.DurationLessThanOneHour().Code));
        });
    }

    // # DURATION LONGER THAN 10 HOURS (INVALID)
    [Test]
    public void Create_WithDurationLongerThanTenHours_ShouldReturnFailureResult()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1).AddHours(8);
        var end = DateTime.Today.AddDays(1).AddHours(20);
        
        // Act
        var result = EventTimeRange.Create(start, end);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTimeRangeError.DurationIsLongerThanTenHours().Code));
        });
    }
}