using Application.AppEntry.Commands.EventCommands;
using Application.AppEntry.Commands.UserCommands;
using Application.Features.EventHandlers;
using Application.Features.UserHandlers;
using Tests.Fakes;
using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event.UC10___Create_user;

[TestFixture]
public class CreateUserHandlerTests
{
    private FakeUserRepository Repo { get; } = new();
    private FakeUnitOfWork Uow { get; } = new();
    
    // # S1 - Command to create user with valid data can be handled
    [Test]
    public async Task Handle_Command_To_Create_User()
    {
        // Arrange
        var command = CreateUserCommand.Create("Lars", "Larsen", "lars@via.dk");
        var handler = new CreateUserHandler(Repo, Uow);
        
        // Act
        var result = await handler.HandleAsync(command);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
        });
    }
    
    // # F1  - There is no fail scenario since a valid CreateUserCommand always can be handled
}