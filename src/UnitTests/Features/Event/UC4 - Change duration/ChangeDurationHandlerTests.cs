using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Common.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Features.Event;

public class ChangeDurationHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    private static DateTime Date => DateTime.Today.AddDays(1);
    
    // # S1 - Time change
    [Test]
    public async Task Create_Command_With_Valid_TimeRange_Should_Succeed()
    {
        // Arrange
        var expectedStart = Date.AddHours(9);
        var expectedEnd = Date.AddHours(10);
        
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        var command = ChangeDurationCommand.Create(id.ToString(), expectedStart, expectedEnd);
        var handler = new ChangeDurationHandler(Repo, Uow);
            
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedTask = Repo.GetByIdAsync(@event.Id);
        var updated = await updatedTask;
        
        // Assert
        Assert.Multiple( () =>
        {
            // Unwrap the updated event
            var timeRange = updated.Duration;
            
            Assert.That(result.IsFailure, Is.False);
            Assert.That(timeRange?.Start, Is.EqualTo(expectedStart));
            Assert.That(timeRange?.End, Is.EqualTo(expectedEnd));
        });
    }
    
    // # F1 - Start date after end date
    [Test]
    public void Given_Start_Date_After_End_Date_Should_Return_Error()
    {
        // Arrange
        var expectedStart = Date.AddDays(1).AddHours(8);
        var expectedEnd = Date.AddHours(10);
        
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var result = ChangeDurationCommand.Create(id.ToString(), expectedStart,expectedEnd);
        var handler = new ChangeDurationHandler(Repo,Uow);
        
        // var result = await handler.HandleAsync(commandResult);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == TimeRangeError.StartAfterEndDate().Code));
        });
    }
}