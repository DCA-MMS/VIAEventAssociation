using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Application.AppEntry.Commands.UserCommands;

public class CreateUserCommand
{
    public FullName FullName { get; }
    public Email Email { get; }
    
    private CreateUserCommand(FullName fullName, Email email)
    {
        FullName = fullName;
        Email = email;
    }
    
    public static Result<CreateUserCommand> Create(string firstName, string lastName, string email)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        var emailResult = Email.Create(email);
        
        if (fullNameResult.IsFailure || emailResult.IsFailure)
        {
            List<Error> errors = [];
            errors.AddRange(fullNameResult.Errors);
            errors.AddRange(emailResult.Errors);
            return Result<CreateUserCommand>.Failure(errors.ToArray());
        }
        
        return Result<CreateUserCommand>.Success(new CreateUserCommand(fullNameResult, emailResult));
    }
}