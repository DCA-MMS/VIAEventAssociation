using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event.UC2___Change_title;

[TestFixture]
public class ChangeTitleCommandTests
{
    private FakeEventRepository EventRepo { get; } = new();    
    
    // # S1 - Command can be created with different valid titles
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public void Create_Command_With_Valid_Title_Should_Succeed(string expected)
    {
        // Arrange
        var @event = EventRepo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var command = ChangeTitleCommand.Create(id.ToString(), expected);
        var result = command.Value;
        string title = result.Title;
                
        // Assert
        Assert.That(title, Is.EqualTo(expected));
    }
    
    // # F1 - Empty title can't be created
    [Test]
    public void Create_Command_With_Empty_Title_Should_Fail()
    {
        // Arrange
        var @event = EventRepo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var result = ChangeTitleCommand.Create(id.ToString(), "");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
        });
    }
}