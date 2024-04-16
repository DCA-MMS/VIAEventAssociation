using Application.AppEntry.Commands.UserCommands;
using Tests.Fakes;

namespace Tests.Features.Event.UC10___Create_user;

[TestFixture]
public class CreateUserCommandTests
{
    private FakeUserRepository UserRepo { get; } = new();
    
    // # S1 - Command to create user with valid data can be created
    [Test]
    public void Create_Command_To_Create_User_With_Valid_Data()
    {
        // Act
        var result = CreateUserCommand.Create("Lars", "Larsen", "lars@via.dk");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
        });
        
    }
    
    // # F1  - Command to create user with invalid data can't be created
    [Test]
    public void Create_Command_To_Create_User_With_Invalid_Data()
    {
        // Act
        var result = CreateUserCommand.Create("Lars", "Larsen", "lars@invalid.email");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
        });
    }
}