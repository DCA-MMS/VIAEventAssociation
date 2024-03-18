using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase1
{
    // # S1
    [Test]
    public void When_Creating_Event_Id_Status_Capacity_Should_Be_Default_Values()
    {
        // Arrange
        const EventStatus status = EventStatus.Draft;
        const int capacity = 5;
        
        // Act
        var result = EventFactory
            .Create()
            .Build();

        Assert.Multiple(() =>
        {
            // Prepare the result data
            var statusResult = result.Status;
            int capacityResult = result.Capacity;
            var id = result.Id;
            
            // Assert
            Assert.That(id, Is.Not.Null);
            Assert.That(statusResult, Is.EqualTo(status));
            Assert.That(capacityResult, Is.EqualTo(capacity));
        });
    }
    
    // # S2
    [Test]
    public void When_Creating_Event_Title_Should_Be_Default_Value()
    {
        // Arrange
        const string title = "Working Title";
        
        // Act
        var result = EventFactory
            .Create()
            .Build();

        // Prepare the result data
        string titleResult = result.Title;
            
        // Assert
        Assert.That(titleResult, Is.EqualTo(title));
    }
    
    // # S3
    [Test]
    public void When_Creating_Event_Description_Should_Be_Default_Value()
    {
        // Arrange
        const string description = "";
        
        // Act
        var result = EventFactory
            .Create()
            .Build();

        // Prepare the result data
        string descriptionResult = result.Description;
            
        // Assert
        Assert.That(descriptionResult, Is.EqualTo(description));
    }
    
    // # S4
    [Test]
    public void When_Creating_Event_Visibility_Should_Be_Default_Value()
    {
        // Arrange
        const EventVisibility visibility = EventVisibility.Private;
        
        // Act
        var result = EventFactory
            .Create()
            .Build();

        // Prepare the result data
        var visibilityResult = result.Visibility;
            
        // Assert
        Assert.That(visibilityResult, Is.EqualTo(visibility));
    }
}