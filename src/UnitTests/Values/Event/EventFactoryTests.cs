using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Values.Event;

[TestFixture]
[Category("EventFactory")]
public class EventFactoryTests
{
   // # Default Values
   [Test,Category("EventFactory - Default Values")]
   public void Create_Empty_Event_Should_Have_Default_Values_Title()
   {
      // Arrange
      const string title = "Working Title";
      
      // Act
      var @event = EventFactory.Create()
         .Build();
      
      string result = @event.Title;
      
      // Assert
      Assert.That(result, Is.EqualTo(title));
   }
   
   [Test,Category("EventFactory - Default Values")]
   public void Create_Empty_Event_Should_Have_Default_Values_Description()
   {
      // Arrange
      const string description = "";
      
      // Act
      var @event = EventFactory.Create()
         .Build();
      
      string result = @event.Description;
      
      // Assert
      Assert.That(result, Is.EqualTo(description));
   }
   
   [Test,Category("EventFactory - Default Values")]
   public void Create_Empty_Event_Should_Have_Default_Values_Status()
   {
      // Arrange
      const EventStatus status = EventStatus.Draft;
      
      // Act
      var @event = EventFactory.Create()
         .Build();
      
      var result = @event.Status;
      
      // Assert
      Assert.That(result, Is.EqualTo(status));
   }
   
   [Test,Category("EventFactory - Default Values")]
   public void Create_Empty_Event_Should_Have_Default_Values_Visibility()
   {
      // Arrange
      const EventVisibility visibility = EventVisibility.Private;
      
      // Act
      var @event = EventFactory.Create()
         .Build();
      
      var result = @event.Visibility;
      
      // Assert
      Assert.That(result, Is.EqualTo(visibility));
   }
   
   // # Change Title
   
   [Test]
   public void Create_Event_With_Title_Should_Have_Title()
   {
      // Arrange
      const string title = "New Title";
      
      // Act
      var @event = EventFactory.Create()
         .WithTitle(title)
         .Build();
      
      string result = @event.Title;
      
      // Assert
      Assert.That(result, Is.EqualTo(title));
   }

   // # Change Description

   [Test] 
   public void Create_Event_With_Description_Should_Have_Description()
      {
         // Arrange
         const string description = "New Description";
         
         // Act
         var @event = EventFactory.Create()
               .WithDescription(description)
               .Build();
         
         string result = @event.Description;
         
         // Assert
         Assert.That(result, Is.EqualTo(description));
      }
   
   
   // # Change Status
   
   [Test]
   public void Create_Event_With_Status_Should_Have_Status()
   {
      // Arrange
      const EventStatus status = EventStatus.Ready;
      
      // Act
      var @event = EventFactory.Create()
         .WithStatus(status)
         .Build();
      
      var result = @event.Status;
      
      // Assert
      Assert.That(result, Is.EqualTo(status));
   }

   // # Change Visibility
   
   [Test]
   public void Create_Event_With_Visibility_Should_Have_Visibility()
   {
      // Arrange
      const EventVisibility visibility = EventVisibility.Private;
      
      // Act
      var @event = EventFactory.Create()
         .WithVisibility(visibility)
         .Build();
      
      var result = @event.Visibility;
      
      // Assert
      Assert.That(result, Is.EqualTo(visibility));
   }

   // # Change Capacity
   
   public void Create_Event_With_Capacity_Of_Six_Should_Have_Capacity_Of_Six()
   {
      // Arrange
      const int capacity = 6;
      
      // Act
      var @event = EventFactory.Create()
         .WithCapacity(capacity)
         .Build();
      
      int result = @event.Capacity;
      
      // Assert
      Assert.That(result, Is.EqualTo(capacity));
   }
   
   // # Multiple Changes
   
   [Test]
   public void Create_Event_With_Multiple_Changes_Should_Have_Multiple_Changes()
   {
      // Arrange
      const string title = "New Title";
      const string description = "New Description";
      const EventStatus status = EventStatus.Ready;
      const EventVisibility visibility = EventVisibility.Public;
      const int capacity = 10;
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .WithDescription(description)
         .WithVisibility(visibility)
         .WithStatus(status)
         .WithCapacity(capacity)
         .Build();
      
      var @event = result;
      Assert.Multiple(() => {
         // Assert
         string titleResult = @event.Title;
         string descriptionResult = @event.Description;
         int capacityResult = @event.Capacity;
         
         Assert.That(titleResult, Is.EqualTo(title));
         Assert.That(descriptionResult, Is.EqualTo(description));
         Assert.That(@event.Status, Is.EqualTo(status));
         Assert.That(@event.Visibility, Is.EqualTo(visibility)); 
         Assert.That(capacityResult, Is.EqualTo(capacity));
      });
   }
   
}