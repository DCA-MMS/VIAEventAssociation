using VIAEventAssociation.Core.Tools.OperationResult;

namespace VIAEventAssociation.Core.Domain.Entities.Values;

public class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }
    
    public static Result<Email> Create(string value)
    {
        return Result<Email>.Success(new Email(value));
    }
}