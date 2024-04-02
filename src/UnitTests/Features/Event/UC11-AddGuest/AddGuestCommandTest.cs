using Application.AppEntry.Commands.EventCommands;
using Tests.Fakes;

namespace Tests.Features.Event.UC11_AddGuest;

public class AddGuestCommandTest
{
    private FakeEventRepository EventRepository { get; } = new();   
    private FakeUserRepository UserRepository { get; } = new();
    
    // # S1
    [Test]
    public void Create_AddGuestCommand_Success()
    {
        // Arrange
        var @event = EventRepository.Events.First();
        var user = UserRepository.Users.First();

        // Act
        var command = AddGuestCommand.Create(@event.Id.Value.ToString(), user.Id.Value.ToString());
      
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(command.Value.EventId.Value, Is.EqualTo(@event.Id.Value));
            Assert.That(command.Value.UserId.Value, Is.EqualTo(user.Id.Value));
        });
    }
}