namespace VIAEventAssociation.Core.Tools.OperationResult;

public class Result
{
    private protected bool IsFailure = false;
    private protected List<string> ErrorMessage = [];
    private protected Result() {}
    
    public static Result Success() => new Result();

    public static Result Failure(string errorMessage)
    {
        var result = new Result
        {
            IsFailure = true
        };
        result.ErrorMessage.Add(errorMessage);
        return result;
    }
}

public class Result<T> : Result
{
    public T Payload { get; init; }

    public static Result<T> Success(T payload) => new()
    {
        Payload = payload
    };

    public new static Result<T> Failure(string errorMessage)
    {
        var result = new Result<T>()
        {
            IsFailure = true
        };
        result.ErrorMessage.Add(errorMessage);
        return result;
    }
}