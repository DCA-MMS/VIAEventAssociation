using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Entities.Values;

public class FullName
{
    public string FirstName { get; }
    public string LastName { get; }


    
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<FullName> Create(string firstName, string lastName)
    {
        return Result<FullName>.Success(new FullName(firstName, lastName));
    }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}