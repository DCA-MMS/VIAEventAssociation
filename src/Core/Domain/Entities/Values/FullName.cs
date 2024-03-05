using System.Globalization;
using System.Text.RegularExpressions;
using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.User;

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
        var errors = Validate(firstName, false);
        errors.AddRange(Validate(lastName, true));
        
        if (errors.Count > 0)
        {
            return Result<FullName>.Failure(errors.ToArray());
        }
        
        var textInfo = new CultureInfo("en-US", false).TextInfo;

        // Convert to title case
        var formattedFirstName = textInfo.ToTitleCase(firstName.ToLower());
        var formattedLastName = textInfo.ToTitleCase(lastName.ToLower());
        
        return Result<FullName>.Success(new FullName(formattedFirstName, formattedLastName));
    }
    
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
    
    private static List<Error> Validate(string name, bool isLastName)
    {
        var errors = new List<Error>();
    
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(isLastName ? UserFullNameError.LastNameIsEmpty() : UserFullNameError.FirstNameIsEmpty());
        }
        
        if (name.Length < 2)
        {
            errors.Add(isLastName ? UserFullNameError.LastNameIsTooShort() : UserFullNameError.FirstNameIsTooShort());
        }
        
        if (name.Length > 25)
        {
            errors.Add(isLastName ? UserFullNameError.LastNameIsTooLong() : UserFullNameError.FirstNameIsTooLong());
        }
        
        var namePattern = isLastName ? "^[a-zA-Z]+(?: [a-zA-Z]+)*$" : "^[a-zA-Z]+$"; 
    
        if (!Regex.IsMatch(name, namePattern))
        {
            errors.Add(isLastName ? UserFullNameError.LastNameIsInvalid() : UserFullNameError.FirstNameIsInvalid());
        }
        
        return errors;
    }
}