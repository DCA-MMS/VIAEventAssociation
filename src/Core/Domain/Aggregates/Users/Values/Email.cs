using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.User;

namespace VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

public class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }
    
    public static Result<Email> Create(string value)
    {
        var errors = Validate(value);

        if (errors.Count > 0)
        {
            return Result<Email>.Failure(errors.ToArray());
        }
        
        return Result<Email>.Success(new Email(value));
    }

    private static List<Error> Validate(string value)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(UserEmailError.EmailIsEmpty());
            return errors;
        }
        
        var email = value.ToLower();

        if (!email.EndsWith("@via.dk", StringComparison.OrdinalIgnoreCase))
        {
            errors.Add(UserEmailError.EmailMustEndWith());
            return errors;
        }

        var splitEmail = email.Split("@via.dk");
        
        if (splitEmail.Length > 2) //Check if the email contains @via.dk more than once
        {
            errors.Add(UserEmailError.EmailMustStartWith());
            return errors;
        }

        var emailStart = splitEmail[0];

        if (!emailStart.All(char.IsLetterOrDigit))
        {
            errors.Add(UserEmailError.EmailWithInvalidCharacters());
            return errors;        
        }
        
        if (!IsValidStartEmail(emailStart))
        {
            errors.Add(UserEmailError.EmailMustStartWith());
            return errors;
        }

        return errors;
    }

    private static bool IsValidStartEmail(string emailStart)
    {
        var allLetters = emailStart.All(char.IsLetter);
        var allDigits = emailStart.All(char.IsDigit);

        if (!allLetters && !allDigits) //Contains both numbers and letters
        {
            return false;
        }

        if (emailStart.Any(char.IsLetter)) //Contains only letters
        {
            if (emailStart.Length is not (3 or 4))
            {
                return false;
            }
        }
        else if (emailStart.Any(char.IsDigit)) //Contains only numbers
        {
            if (emailStart.Length != 6)
            {
                return false;
            }
        }
        else //Contains no numbers or letters
        {
            return false;
        }
        
        return true;
    }
    
}