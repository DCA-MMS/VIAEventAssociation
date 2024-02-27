using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace VIAEventAssociation.Core.Tools.OperationResult;

public class Result
{
    // - PROPERTIES
    /// <summary>
    /// Flag that indicate if the result is a failure
    /// (Default value is false)
    /// </summary>
    private bool _isFailure;
    
    /// <summary>
    /// Array of error messages
    /// </summary>
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
    /// <summary>
    /// Flag that indicate if the result is a failure
    /// (Default value is false)
    /// </summary>
    private bool _isFailure;
    
    /// <summary>
    /// Array of error messages
    /// </summary>
    private Error[] _errorMessages = [];
    
    /// <summary>
    /// The value of the result
    /// </summary>
    public T Value { get; private init; } = default!;
    
    // - IMPLICIT CONVERSION OPERATORS
    // This operator is used to convert a T to a Result<T>
    public static implicit operator Result<T>(T value) => Success(value);
    
    // This operator is used to convert a Result<T> to a T
    public static implicit operator T(Result<T> result) => result.Value;
    
    // - FACTORY METHOD - Successful Result
    // This method is used to create a new instance of Result with the _isFailure property set to false
    public static Result<T> Success(T value) => new() { Value = value };
    
    // - FACTORY METHOD - Failed Result
    // This method is used to create a new instance of Result with the _isFailure property set to true
    public static Result<T> Failure(params Error[] errorMessages) => new() { _isFailure = true, _errorMessages = errorMessages };
    
    // * Indicates if the result is a failure
    public bool IsFailure => _isFailure;
    
    public IEnumerable<Error> Errors => _errorMessages;
}
