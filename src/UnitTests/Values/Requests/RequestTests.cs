using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Request.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace Tests.Values.Requests;

[TestFixture]
public class RequestTests
{
    private readonly UserId _userId = new ();

    [Test, Category("Request")]
    [TestCase(RequestStatus.Pending)]
    [TestCase(RequestStatus.Rejected)]
    [TestCase(RequestStatus.Accepted)]
    public void Success_Create_Request(RequestStatus status)
    {
        // Arrange
        var request = Request.Create(_userId, status);
            
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(request.IsFailure, Is.False);
            Assert.That(request.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(request.Value.Status, Is.EqualTo(status));
        });
    }
    
    [Test, Category("Request")]
    [TestCase(RequestStatus.Pending)]
    public void Success_Approve_Pending_Request(RequestStatus status)
    {
        // Arrange
        var request = Request.Create(_userId, status);
            
        // Act
        var result = request.Value.Approve();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(request.IsFailure, Is.False);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(request.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(request.Value.Status, Is.EqualTo(RequestStatus.Accepted));
        });
    }
    
        
    [Test, Category("Request")]
    [TestCase(RequestStatus.Pending)]
    public void Success_Decline_Pending_Request(RequestStatus status)
    {
        // Arrange
        var request = Request.Create(_userId, status);
            
        // Act
        var result = request.Value.Decline();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(request.IsFailure, Is.False);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(request.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(request.Value.Status, Is.EqualTo(RequestStatus.Rejected));
        });
    }
}
