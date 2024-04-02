using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class ChangeDescriptionAggregateTests
{
    
    // # S1
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    public void Given_An_Event_Changing_The_Description_Into_Another_Description_Then_The_Description_Should_Be_Updated(string expected)
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.ChangeDescription(expected);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            string description = @event.Description;
            var status = @event.Status;
            
            // Assert
            Assert.That(description, Is.EqualTo(expected));
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # S2
    [Test]
    public void Given_An_Event_Changing_The_Description_Into_Another_Description_Then_The_Description_Should_Be_Empty()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        @event.ChangeDescription("");
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            string description = @event.Description;
            var status = @event.Status;
            
            // Assert
            Assert.That(description, Is.EqualTo(""));
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    [Test]
    [TestCase("Scary Movie Night!")]
    [TestCase("Graduation Gala")]
    [TestCase("VIA Hackathon")]
    // # S3
    public void Given_Event_With_Ready_Status_Changing_Description_Should_Make_Status_Back_To_draft(string expected)
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Ready)
            .Build();
        
        // Act
        @event.ChangeDescription(expected);
        
        Assert.Multiple(() =>
        {
            // Prepare for result data
            string description = @event.Description;
            var status = @event.Status;
            
            // Assert
            Assert.That(description, Is.EqualTo(expected));
            Assert.That(status, Is.EqualTo(EventStatus.Draft));
        });
    }
    
    // # F1
    [Test]
    public void Given_Event_Changing_The_Description_To_A_Too_Long_Description_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeDescription("Imagine a grand celebration of technology and innovation, the \"TechFuture Gala 2024,\" set in a sleek, modern convention center adorned with cutting-edge gadgets and interactive displays. This day-long event, scheduled for July 15th, 2024, promises to gather industry leaders, tech enthusiasts, and innovators from around the globe. Attendees will have the opportunity to engage in thought-provoking keynotes, participate in hands-on workshops on the latest technological advancements, and network with peers in specially designed meeting zones. Highlights include a keynote speech from a renowned tech visionary, unveiling of groundbreaking products, and a startup showcase where emerging companies present their solutions to pressing global challenges. The gala will conclude with an awards ceremony recognizing outstanding achievements in technology and innovation, followed by a futuristic-themed networking dinner under the stars. This event is a must-attend for anyone passionate about shaping the future of technology.");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.IsTooLong().Code));
        });
    }
    
    // # F2
    [Test]
    public void Given_Event_With_Active_Status_Changing_Description_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Active)
            .Build();
        
        // Act
        var result = @event.ChangeDescription("Scary Movie Night!");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.NotModifiable().Code));
        });
    }
    
    // # F3
    [Test]
    public void Given_Event_With_Cancelled_Status_Changing_Description_Should_Return_An_Error()
    {
        // Arrange
        var @event = EventFactory
            .Create()
            .WithStatus(EventStatus.Cancelled)
            .Build();
        
        // Act
        var result = @event.ChangeDescription("Scary Movie Night!");
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventDescriptionError.NotModifiable().Code));
        });
    }
}
