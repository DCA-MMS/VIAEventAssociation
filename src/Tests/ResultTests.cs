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
    public void Implicit_Conversion_From_T_To_Result_T()
    {
        // Arrange
        var value = 10;
        
        // Act
        Result<int> result = value;
        
        // Assert
        Assert.That(result.Value, Is.EqualTo(value));
    }
    
    [Test, Category("Result")]
    public void Implicit_Conversion_From_Result_T_To_T()
    {
        // Arrange
        var value = 10;
        var result = Result<int>.Success(value);
        
        // Act
        int resultValue = result;
        
        // Assert
        Assert.That(resultValue, Is.EqualTo(value));
    }
}
