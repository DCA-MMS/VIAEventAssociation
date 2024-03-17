using VIAEventAssociation.Core.Domain.Aggregates.Event;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.Event;

namespace Tests.Features.Event;

[TestFixture]
public class Usecase7
{
    // # S1
    [Test]
    [TestCase(EventStatus.Draft, 5)]
    [TestCase(EventStatus.Draft, 10)]
    [TestCase(EventStatus.Draft, 25)]
    [TestCase(EventStatus.Draft, 50)]
    [TestCase(EventStatus.Ready, 5)]
    [TestCase(EventStatus.Ready, 10)]
    [TestCase(EventStatus.Ready, 25)]
    [TestCase(EventStatus.Ready, 50)]
    public void Set_Capacity_Below_Or_Equal_To_50_With_Status_Draft_Or_Ready_Should_Succeed(EventStatus status, int capacity)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(EventStatus.Draft).Build();
        
        // Act
        var result = @event.ChangeCapacity(capacity);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That((int)@event.Capacity, Is.LessThanOrEqualTo(50));
            Assert.That((int)@event.Capacity, Is.EqualTo(capacity));
        });
    }
    
    // # S2
    [Test]
    [TestCase(EventStatus.Draft, 5)]
    [TestCase(EventStatus.Draft, 10)]
    [TestCase(EventStatus.Draft, 25)]
    [TestCase(EventStatus.Draft, 50)]
    [TestCase(EventStatus.Ready, 5)]
    [TestCase(EventStatus.Ready, 10)]
    [TestCase(EventStatus.Ready, 25)]
    [TestCase(EventStatus.Ready, 50)]
    public void Set_Capacity_Above_Or_Equal_To_5_With_Status_Draft_Or_Ready_Should_Succeed(EventStatus status, int capacity)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(EventStatus.Draft).Build();
        
        // Act
        var result = @event.ChangeCapacity(capacity);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That((int)@event.Capacity, Is.GreaterThanOrEqualTo(5));
            Assert.That((int)@event.Capacity, Is.EqualTo(capacity));
        });
    }
    
    // # S3
    [Test]
    [TestCase(5, 10)]
    [TestCase(25, 30)]
    [TestCase(45, 50)]
    public void Set_Capacity_In_Range_5_To_50_With_Status_Active_And_New_Capacity_Is_Greater_Than_Previous_Should_Succeed(int previousCapacity, int newCapacity)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(EventStatus.Active).WithCapacity(previousCapacity).Build();
        
        // Act
        var result = @event.ChangeCapacity(newCapacity);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That((int)@event.Capacity, Is.GreaterThanOrEqualTo(5));
            Assert.That((int)@event.Capacity, Is.LessThanOrEqualTo(50));
            Assert.That((int)@event.Capacity, Is.EqualTo(newCapacity));
        });
    }

    // # F1
    [Test]
    [TestCase(10, 5)]
    [TestCase(30, 25)]
    [TestCase(50, 45)]
    public void Reduce_Capacity_With_Status_Active_Should_Fail(int previousCapacity, int newCapacity)
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(EventStatus.Active).WithCapacity(previousCapacity).Build();
        
        // Act
        var result = @event.ChangeCapacity(newCapacity);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.CantReduceCapacityError().Code));
        });
    }
    
    // # F2
    [Test]
    public void Change_Capacity_With_Status_Cancelled_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create().WithStatus(EventStatus.Cancelled).Build();
        
        // Act
        var result = @event.ChangeCapacity(10);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.NotModifiable().Code));
        });
    }
    
    // # F3
    //TODO: Add this test when it's possible to add a location to an event
    
    // # F4
    [Test]
    public void Set_Capacity_Below_5_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeCapacity(4);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.IsLessThanFive().Code));
        });
    }
    
    // # F5
    [Test]
    public void Set_Capacity_Above_50_Should_Fail()
    {
        // Arrange
        var @event = EventFactory.Create().Build();
        
        // Act
        var result = @event.ChangeCapacity(51);
        
        // Assert
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.ToList(), Has.Exactly(1).Matches<Error>(x => x.Code == EventCapacityError.IsGreaterThanFifty().Code));
        });
    }
}