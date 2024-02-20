using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Tools.OperationResult;

public class Result
{
    // - PROPERTIES
    private bool _isFailure = false;
    private Error[] _errorMessages = [];

    // * FACTORY METHOD - Successful Result
    // * This method is used to create a new instance of Result with the _isFailure property set to false
    public static Result Success() => new();
    
    // * FACTORY METHOD - Failed Result
    // * This method is used to create a new instance of Result with the _isFailure property set to true
    public static Result Failure(params Error[] errorMessages) => new() { _isFailure = true, _errorMessages = errorMessages };
    
    public bool IsFailure => _isFailure;
    
    public IEnumerable<Error> Errors => _errorMessages;
}

public class Result<T>
{
    // - PROPERTIES
    private bool _isFailure = false;
    private Error[] _errorMessages = [];
    public T Value { get; private init; } = default!;
    
    // - IMPLICIT CONVERSION OPERATOR
    // This operator is used to convert a T to a Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
    
    // - FACTORY METHOD - Successful Result
    // This method is used to create a new instance of Result with the _isFailure property set to false
    public static Result<T> Success(T value) => new() { Value = value };
    
    // - FACTORY METHOD - Failed Result
    // This method is used to create a new instance of Result with the _isFailure property set to true
    public new static Result<T> Failure(params Error[] errorMessages) => new() { _isFailure = true, _errorMessages = errorMessages };
    
    // * Indicates if the result is a failure
    public new bool IsFailure => _isFailure;
    
    public new IEnumerable<Error> Errors => _errorMessages;
}
