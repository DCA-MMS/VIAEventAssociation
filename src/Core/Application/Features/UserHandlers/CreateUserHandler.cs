using Application.AppEntry.Commands.UserCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.Features.UserHandlers;

internal class CreateUserHandler(IUserRepository repository, IUnitOfWork uow) : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> HandleAsync(CreateUserCommand command)
    {
        var userResult = User.Create(command.FullName, command.Email);
        
        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Errors.ToArray());
        }
        
        await repository.AddAsync(userResult.Value);
        await uow.SaveChangesAsync();
        
        return Result.Success();
    }
}