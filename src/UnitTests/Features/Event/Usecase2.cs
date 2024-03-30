using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase2
{
    // # S1
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public void Given_An_Event_Changing_The_Title_Into_Another_Title_Then_The_Title_Should_Be_Updated(string expected)
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        
        // Act
        @event.ChangeTitle(expected);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            string title = @event.Title;
            var status = @event.Status;
            
            // Assert
            Assert.That(title, Is.EqualTo(expected));
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S1
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public void Given_Event_With_Ready_Status_Changing_Title_Should_Make_Status_Back_To_draft(string expected)
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Ready)
            .Build();
        
        
        // Act
        @event.ChangeTitle(expected);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            string title = @event.Title;
            var status = @event.Status;
            
            // Assert
            Assert.That(title, Is.EqualTo(expected));
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # F1
    [Test]
    public void Given_Event_Changing_The_Title_To_An_Empty_String_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeTitle("");
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
        });
    }
    
    // # F2
    [Test]
    [TestCase("XY")]
    [TestCase("a")]
    public void Given_Event_Changing_The_Title_To_A_String_That_Is_Too_Short_Should_Return_An_Error(string title)
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeTitle(title);
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooShort().Code));
        });
    }
    
    // # F3
    [Test]
    public void Given_Event_Changing_The_Title_To_A_String_That_Is_Too_Long_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeTitle("This is a very long title that is longer than 75 characters, which is the maximum length of a title");
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsTooLong().Code));
        });
    }
    
    // # F4 - This test is not needed
     [Test]
     public void Given_Event_With_No_Title_Changing_Title_Should_Return_An_Error()
     {
         // Arrange
         var @event = EventFactory
             .Create()
             .Build();
         
         // Act
         var result = @event.ChangeTitle(null!);
         Assert.Multiple(() =>
         {

             // Assert
             Assert.That(result.IsFailure, Is.True);
             Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.IsEmpty().Code));
         });
     }

    // # F5
    [Test]
    public void Given_Event_With_Active_Status_Changing_Title_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Active)
            .Build();
        
        // Act
        var result = @event.ChangeTitle("New Title");
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.NotModifiable().Code));
        });
    }
    
    // # F6
    [Test]
    public void Given_Event_With_Cancelled_Status_Changing_Title_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        // Act
        var result = @event.ChangeTitle("New Title");
        
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventTitleError.NotModifiable().Code));
        });
    }
}