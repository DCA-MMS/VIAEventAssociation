using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Dispatcher;
using Tests.Fakes.CommandHandlers.EventHandlers;

namespace Tests.Features.Event.UC1___Create_event;

[TestFixture]
public class CreateEventDispatcherInteractionTests
{
    [Test]
    public void Dispatcher_Test()
    {
        // Arrange
        var command = new CreateEventCommand();
        var fakeHandler = new FakeCreateEventHandler();
        var commandDispatcher = new CommandDispatcher(/* What to put here??? */);

        // Act
        commandDispatcher.DispatchAsync(command);
        
        // Assert
        Assert.IsTrue(fakeHandler.CommandWasHandled);
    }
}