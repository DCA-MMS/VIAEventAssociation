using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Values.Event;

[TestFixture]
[Category("Event Capacity")]
public class EventCapacityTests
{
    // # NEGATIVE (-1) - INVALID
    [Test]
    public void Negative_EventCapacity_Should_Be_Invalid()
    {
        // Arrange
        var value = -1;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    // # NEGATIVE - Returns correct error
    [Test]
    public void Negative_EventCapacity_Should_Return_NegativeError()
    {
        // Arrange
        var value = -1;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.IsNegative().Code));
    }
    
    // # EMPTY (0) - INVALID
    [Test]
    public void Zero_EventCapacity_Should_Be_Invalid()
    {
        // Arrange
        var value = 0;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    // # EMPTY - Returns correct error 
    [Test]
    public void Zero_EventCapacity_Should_Return_ZeroError()
    {
        // Arrange
        var value = 0;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.IsLessThanOne().Code));
    }
    
    // # BOTTOM LIMIT (1) - VALID
    [Test]
    public void One_EventCapacity_Should_Be_Valid()
    {
        // Arrange
        var value = 1;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # VALID (5)
    [Test]
    public void Five_EventCapacity_Should_Be_Valid()
    {
        // Arrange
        var value = 5;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # TOP LIMIT (1000) - VALID
    [Test]
    public void OneThousand_EventCapacity_Should_Be_Valid()
    {
        // Arrange
        var value = 1000;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # TOO HIGH (1001) - INVALID
    [Test]
    public void OneThousandOne_EventCapacity_Should_Be_Invalid()
    {
        // Arrange
        var value = 1001;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    // # TOO HIGH - Returns correct error
    [Test]
    public void OneThousandOne_EventCapacity_Should_Return_TooHighError()
    {
        // Arrange
        var value = 1001;
        
        // Act
        var result = EventCapacity.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.IsGreaterThanAThousand().Code));
    }
}