using Application.AppEntry.Commands.EventCommands;
using Application.Features.EventHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class ChangeDescriptionHandlerTests
{
    private FakeEventRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
        
    // # S1 - Title is changed on the given event!
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public async Task Given_An_ChangeDescriptionCommand_The_Handler_Changes_The_Title(string expected)
    {
        // Arrange
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        var command = ChangeDescriptionCommand.Create(id.ToString(), expected);
        var handler = new ChangeDescriptionHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        var updatedTask = Repo.GetByIdAsync(@event.Id);
        var updated = await updatedTask;
        
        // Assert
        Assert.Multiple( () =>
        {
            // Unwrap the updated event
            string description = updated.Description;
            
            Assert.That(result.IsFailure, Is.False);
            Assert.That(description, Is.EqualTo(expected));
        });
    }
    
    // # F1
    [Test]
    public void Given_An_ChangeDescriptionCommand_With_Too_long_Description_The_Handler_Fails()
    {
        // Arrange
        var @event = Repo.Events.First();
        Guid id = @event.Id;
        
        // Act
        var result = ChangeDescriptionCommand.Create(id.ToString(),"Imagine a grand celebration of technology and innovation, the \"TechFuture Gala 2024,\" set in a sleek, modern convention center adorned with cutting-edge gadgets and interactive displays. This day-long event, scheduled for July 15th, 2024, promises to gather industry leaders, tech enthusiasts, and innovators from around the globe. Attendees will have the opportunity to engage in thought-provoking keynotes, participate in hands-on workshops on the latest technological advancements, and network with peers in specially designed meeting zones. Highlights include a keynote speech from a renowned tech visionary, unveiling of groundbreaking products, and a startup showcase where emerging companies present their solutions to pressing global challenges. The gala will conclude with an awards ceremony recognizing outstanding achievements in technology and innovation, followed by a futuristic-themed networking dinner under the stars. This event is a must-attend for anyone passionate about shaping the future of technology.");
        var handler = new ChangeDescriptionHandler(Repo, Uow);
        
        // var result = await handler.HandleAsync(commandResult);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.IsTooLong().Code));
        });
    }
}