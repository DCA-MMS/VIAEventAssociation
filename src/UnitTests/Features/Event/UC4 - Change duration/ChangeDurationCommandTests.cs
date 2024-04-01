using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event;

[TestFixture]
public class ChangeDurationCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();
    private static DateTime Date => DateTime.Today.AddDays(1);
    
    // # S1 - Time change
    [Test]
    public void Create_Command_With_Valid_TimeRange_Should_Succeed()
    {
        // Arrange
        var expectedStart = Date.AddHours(9);
        var expectedEnd = Date.AddHours(10);
        
        var @event = EventRepo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var command = ChangeDurationCommand.Create(id.ToString(), expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            var result = command.Value.Duration;
            
            // Assert
            Assert.That(result?.Start, Is.EqualTo(expectedStart));
            Assert.That(result?.End, Is.EqualTo(expectedEnd));
        });
    }
    
    // # F1 - Start date after end date
    [Test]
    public void Given_Start_Date_After_End_Date_Should_Return_Error()
    {
        // Arrange
        var expectedStart = Date.AddDays(1).AddHours(8);
        var expectedEnd = Date.AddHours(10);
        
        var @event = EventRepo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var result = ChangeDurationCommand.Create(id.ToString(), expectedStart, expectedEnd);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == TimeRangeError.StartAfterEndDate().Code));
        });
    }
}