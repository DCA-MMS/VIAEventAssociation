using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class ChangeTitleHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Title is changed on the given event!
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public async Task Given_An_ChangeTitleCommand_The_Handler_Changes_The_Title(string expected)
    {
        // Arrange
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        var command = ChangeTitleCommand.Create(id.ToString(), expected);
        var handler = new ChangeTitleHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedTask = Repo.GetByIdAsync(@event.Id);
        var updated = await updatedTask;
        
        // Assert
        Assert.Multiple( () =>
        {
            // Unwrap the updated event
            string title = updated.Title;
            
            Assert.That(result.IsFailure, Is.False);
            Assert.That(title, Is.EqualTo(expected));
        });
    }
    
    // # F1 - Empty title can't be created
    [Test]
    public void Given_An_ChangeTitleCommand_With_Empty_Title_The_Handler_Fails()
    {
        // Arrange
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var commandResult = ChangeTitleCommand.Create(id.ToString(), "");
        var handler = new ChangeTitleHandler(Repo, Uow);
        
        // var result = await handler.HandleAsync(commandResult);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(commandResult.IsFailure, Is.True);
            Assert.That(commandResult.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
        });
    }
}