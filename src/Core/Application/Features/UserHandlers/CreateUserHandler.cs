using Application.AppEntry.Commands.UserCommands;
using Application.AppEntry.Interfaces;
using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Common;
using VIAEventAssociation.Core.Tools.OperationResult;

namespace Application.Features.UserHandlers;

public class CreateUserHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    internal CreateUserHandler(IUserRepository repository, IUnitOfWork uow) => (_repository, _unitOfWork) = (repository, uow);
    
    public async Task<Result> HandleAsync(CreateUserCommand command)
    {
        var userResult = User.Create(command.FullName, command.Email);
        
        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Errors.ToArray());
        }
        
        await _repository.AddAsync(userResult.Value);
        await _unitOfWork.SaveChangesAsync();
        
        return Result.Success();
    }
}