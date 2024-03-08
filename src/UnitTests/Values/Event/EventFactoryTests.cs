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
      
      string result = @event.Value.Title;
      
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
      
      string result = @event.Value.Description;
      
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
      
      var result = @event.Value.Status;
      
      // Assert
      Assert.That(result, Is.EqualTo(status));
   }
   
   [Test,Category("EventFactory - Default Values")]
   public void Create_Empty_Event_Should_Have_Default_Values_Visibility()
   {
      // Arrange
      const EventVisibility visibility = EventVisibility.Public;
      
      // Act
      var @event = EventFactory.Create()
         .Build();
      
      var result = @event.Value.Visibility;
      
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
      
      string result = @event.Value.Title;
      
      // Assert
      Assert.That(result, Is.EqualTo(title));
   }

   [Test]
   public void Create_Event_With_Invalid_Title_Empty()
   {
      // Arrange
      const string title = "";
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .Build();
      
      // Assert
      Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
   }
   
   [Test]
   public void Create_Event_With_Invalid_Title_Too_Short()
   {
      // Arrange
      const string title = "AB";
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .Build();
      
      // Assert
      Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooShort().Code));
   }
   
   [Test]
   public void Create_Event_With_Invalid_Title_Too_Long()
   {
      // Arrange
      string title = "A".PadRight(76, 'A');
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .Build();
      
      // Assert
      Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooLong().Code));
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
         
         string result = @event.Value.Description;
         
         // Assert
         Assert.That(result, Is.EqualTo(description));
      }
   
   [Test]
   public void Create_Event_With_Invalid_EventDescription_Too_Long()
   {
      // Arrange
      string description = "A".PadRight(1001, 'A');
      
      // Act
      var result = EventFactory.Create()
         .WithDescription(description)
         .Build();
      
      // Assert
      Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.IsTooLong().Code));
   }
   
   // # Change Status
   
   [Test]
   public void Create_Event_With_Status_Should_Have_Status()
   {
      // Arrange
      const EventStatus status = EventStatus.Published;
      
      // Act
      var @event = EventFactory.Create()
         .WithStatus(status)
         .Build();
      
      EventStatus result = @event.Value.Status;
      
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
      
      EventVisibility result = @event.Value.Visibility;
      
      // Assert
      Assert.That(result, Is.EqualTo(visibility));
   }

   // # Multiple Changes
   
   [Test]
   public void Create_Event_With_Multiple_Changes_Should_Have_Multiple_Changes()
   {
      // Arrange
      const string title = "New Title";
      const string description = "New Description";
      const EventStatus status = EventStatus.Published;
      const EventVisibility visibility = EventVisibility.Private;
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .WithDescription(description)
         .WithStatus(status)
         .WithVisibility(visibility)
         .Build();
      
      var @event = result.Value;
      Assert.Multiple(() => {
         // Assert
         string titleResult = @event.Title;
         string descriptionResult = @event.Description;
         
         Assert.That(titleResult, Is.EqualTo(title));
         Assert.That(descriptionResult, Is.EqualTo(description));
         Assert.That(@event.Status, Is.EqualTo(status));
         Assert.That(@event.Visibility, Is.EqualTo(visibility)); 
      });
   }

   [Test]
   public void Create_Event_With_Multiple_Change_But_Invalid_Title_Should_Fail()
   {
      // Arrange
      const string title = "AB";
      const string description = "New Description";
      const EventStatus status = EventStatus.Published;
      const EventVisibility visibility = EventVisibility.Private;
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .WithDescription(description)
         .WithStatus(status)
         .WithVisibility(visibility)
         .Build();
      
      // Assert
      Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooShort().Code));
   }
   
   [Test]
   public void Create_Event_With_Multiple_Change_But_Invalid_Title_And_Description_Should_Fail()
   {
      // Arrange
      const string title = "AB";
      string description = "A".PadRight(1001, 'A');
      const EventStatus status = EventStatus.Published;
      const EventVisibility visibility = EventVisibility.Private;
      
      // Act
      var result = EventFactory.Create()
         .WithTitle(title)
         .WithDescription(description)
         .WithStatus(status)
         .WithVisibility(visibility)
         .Build();
      
      
      // Assert
      Assert.Multiple(() => {
         // Assert
         Assert.That(result.Errors.ToList(), Has.Count.EqualTo(2));
         Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooShort().Code));
         Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.IsTooLong().Code));
      });
   }
   
}