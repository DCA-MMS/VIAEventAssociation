using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Tools.OperationResult;

public class Result
{
    // - PROPERTIES
    private bool _isFailure = false;
    private List<Error> _errorMessages = [];

    // * FACTORY METHOD - Successful Result
    // * This method is used to create a new instance of Result with the _isFailure property set to false
    public static Result Success() => new Result();
    
    // * FACTORY METHOD - Failed Result
    // * This method is used to create a new instance of Result with the _isFailure property set to true
    public static Result Failure(Error errorMessage) => new Result() { _isFailure = true, _errorMessages = [errorMessage] };
    
    public void AppendError(Error errorMessage) => _errorMessages.Add(errorMessage);
    
    public bool IsFailure => _isFailure;
    
    public List<Error> GetErrors() => _errorMessages;
}

public class Result<T> : Result
{
    // - PROPERTIES
    private bool _isFailure = false;
    private List<Error> _errorMessages = [];
    public T Value { get; private init; }
    
    // - IMPLICIT CONVERSION OPERATORS
    // This operator is used to convert a T to a Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
    
    // This operator is used to convert a Result<T> to a T
    public static implicit operator T(Result<T> result) => result.Value;
    
    // - FACTORY METHOD - Successful Result
    // This method is used to create a new instance of Result with the _isFailure property set to false
    public static Result<T> Success(T value) => new Result<T>() { Value = value };
    
    // - FACTORY METHOD - Failed Result
    // This method is used to create a new instance of Result with the _isFailure property set to true
    public new static Result<T> Failure(Error errorMessage) => new Result<T>() { _isFailure = true, _errorMessages = [errorMessage] };
    
    // * Add another error message to the list of error messages
    public new void AppendError(Error errorMessage) => _errorMessages.Add(errorMessage);
    
    // * Indicates if the result is a failure
    public new bool IsFailure => _isFailure;
    
    public new List<Error> GetErrors() => _errorMessages;
}
