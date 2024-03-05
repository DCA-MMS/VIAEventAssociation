using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Values.Event;

[TestFixture]
[Category("Event Title")]
public class EventTitleTests
{
    // # EMPTY
    [Test]
    public void Empty_EventTitle_Should_Be_Invalid()
    {
        // Arrange
        var value = string.Empty;
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    [Test]
    public void Empty_Title_Should_Return_EmptyError()
    {
        // Arrange
        var value = string.Empty;
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
    }
    
    // # TOO SHORT
    [Test]
    public void Two_Character_EventTitle_Should_Be_Invalid()
    {
        // Arrange
        var value = "AB";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }

    [Test]
    public void Two_Character_EventTitle_Should_Return_TooShortError()
    {
        // Arrange
        var value = "AB";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooShort().Code));
    }
    
    // # LIMIT
    [Test]
    public void Three_Character_EventTitle_Should_Be_Valid()
    {
        // Arrange
        var value = "ABC";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    [Test]
    public void Seventy_Five_Character_EventTitle_Should_Be_Valid()
    {
        // Arrange
        var value = "2024 Global Digital Transformation Conference: Bridging Innovations";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    // # VALID (Between 3 and 75 characters)
    
    
    // # TOO LONG
    
    [Test]
    public void Seventy_Seven_Character_EventTitle_Should_Be_Invalid()
    {
        // Arrange
        var value = "2024 Global Digital Transformation Conference: Bridging Innovations and Transforming the Future 2";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    [Test]
    public void Seventy_Seven_Character_EventTitle_Should_Return_TooLongError()
    {
        // Arrange
        var value = "2024 Global Digital Transformation Conference: Bridging Innovations and Transforming the Future 2";
        
        // Act
        var result = EventTitle.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooLong().Code));
    }
}