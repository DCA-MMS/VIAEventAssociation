using VIAEventAssociation.Core.Tools.OperationResult;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests;

[TestFixture]
public class ResultTests
{
    [Test, Category("Result")]
    public void Success_Result_IsFailure_Should_Be_False()
    {
        // Arrange
        var result = Result.Success();
        
        // Act
        var isFailure = result.IsFailure;
        
        // Assert
        Assert.That(isFailure, Is.False);
    }
    
    [Test, Category("Result")]
    public void Failure_Result_IsFailure_Should_Be_True()
    {
        // Arrange
        var result = Result.Failure(UserError.UserNotFound());
        
        // Act
        var isFailure = result.IsFailure;
        
        // Assert
        Assert.That(isFailure, Is.True);
    }
    
    [Test, Category("Result")]
    public void Failure_Result_AppendError_Should_Add_Error_Message()
    {
        // Arrange
        var result = Result.Failure(UserError.UserNotFound());
        
        // Act
        result.AppendError(UserError.UserAlreadyExists());
        
        // Assert
        Assert.That(result.GetErrors().Count, Is.EqualTo(2));
    }
}

[TestFixture]
public class ResultsOfTTests
{
    [Test, Category("Result")]
    public void Success_Result_Create_Result_With_Value()
    {
        // Arrange
        var value = 10;
        
        // Act
        var result = Result<int>.Success(value);
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(value));
    }
    
    [Test, Category("Result")]
    public void Success_Result_Create_Result_With_Value_IsFailure_Should_Be_False()
    {
        // Arrange
        var value = 10;
        
        // Act
        var result = Result<int>.Success(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    [Test, Category("Result")]
    public void Failure_Result_Create_Result_With_Error_Message_IsFailure_Should_Be_True()
    {
        // Arrange
        var userNotFound = UserError.UserNotFound();
        
        // Act
        var result = Result<int>.Failure(userNotFound);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    [Test, Category("Result")]
    public void Failure_Result_AppendError_Should_Add_Error_Message()
    {
        // Arrange
        var result = Result<int>.Failure(UserError.UserNotFound());
        
        // Act
        result.AppendError(UserError.UserAlreadyExists());
        
        // Assert
        Assert.That(result.GetErrors().Count, Is.EqualTo(2));
    }
}
