using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Values;

[TestFixture]
public class TimeRangeTests
{
    [Test]
    public void Start_Before_End_Should_Be_Success()
    {
        // Arrange
        var start = DateTime.Now;
        var end = start.AddSeconds(1);
        
        // Act
        var result = TimeRange.Create(start, end);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }

    [Test]
    public void End_Before_Start_Should_Be_Failure()
    {
        // Arrange
        var start = DateTime.Today.AddDays(1);
        var end = DateTime.Today;
        
        // Act
        var result = TimeRange.Create(start, end);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.First().Code, Is.EqualTo(ErrorCode.TimeRangeStartAfterEndDate));
        });
    }
    
    [Test]
    public void End_Equal_To_Start_Should_Be_Failure()
    {
        // Arrange
        var start = DateTime.Today.AddHours(1);
        var end = DateTime.Today;
        
        // Act
        var result = TimeRange.Create(start, end);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.First().Code, Is.EqualTo(ErrorCode.TimeRangeStartAfterEndTime));
        });
    }
}