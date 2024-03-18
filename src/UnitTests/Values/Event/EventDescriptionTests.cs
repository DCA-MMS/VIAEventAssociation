using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Values.Event;

[TestFixture]
[Category("Event Description")]
public class EventDescriptionTests
{
    [Test]
    public void Empty_EventDescription_Should_Be_Valid()
    {
        // Arrange
        var value = string.Empty;
        
        // Act
        var result = EventDescription.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    [Test]
    public void Valid_EventDescription_Should_Be_Valid()
    {
        // Arrange
        var value = "Annual Tech Innovators Meetup";
        
        // Act
        var result = EventDescription.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.False);
    }
    
    [Test]
    public void Invalid_EventDescription_Should_Be_Invalid()
    {
        // Arrange
        var value = "Annual Tech Innovators Meetup: A day-long event bringing together technology enthusiasts, industry leaders, and emerging innovators. Expect workshops, keynotes, and networking sessions focused on the latest tech trends and future predictions. Open to all tech professionals.";
        
        // Act
        var result = EventDescription.Create(value);
        
        // Assert
        Assert.That(result.IsFailure, Is.True);
    }

    [Test]
    public void Invalid_EventDescription_Should_Have_One_Error()
    {
        // Arrange
        var value = "Annual Tech Innovators Meetup: A day-long event bringing together technology enthusiasts, industry leaders, and emerging innovators. Expect workshops, keynotes, and networking sessions focused on the latest tech trends and future predictions. Open to all tech professionals.";
        
        // Act
        var result = EventDescription.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Count.EqualTo(1));
    }

    [Test]
    public void Invalid_EventDescription_Should_Have_TooLongDescriptionError()
    {
        // Arrange
        var value = "Annual Tech Innovators Meetup: A day-long event bringing together technology enthusiasts, industry leaders, and emerging innovators. Expect workshops, keynotes, and networking sessions focused on the latest tech trends and future predictions. Open to all tech professionals.";
        
        // Act
        var result = EventDescription.Create(value);
        
        // Assert
        Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.IsTooLong().Code));
        
    }
}