using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation;
using VIAEventAssociation.Core.Domain.Aggregates.Event.Entities.Invitation.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;

namespace Tests.Values.Invitations;

[TestFixture]
public class InvitationTests
{
    private readonly UserId _userId = new ();

    [Test, Category("Invitation")]
    [TestCase(InvitationStatus.Pending)]
    [TestCase(InvitationStatus.Rejected)]
    [TestCase(InvitationStatus.Accepted)]
    public void Success_Create_Invitation(InvitationStatus status)
    {
        // Arrange
        var invitation = Invitation.Create(_userId, status);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invitation.IsFailure, Is.False);
            Assert.That(invitation.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(invitation.Value.Status, Is.EqualTo(status));
        });
    }

    [Test, Category("Invitation")]
    [TestCase(InvitationStatus.Pending)]
    public void Success_Accept_Pending_Invitation(InvitationStatus status)
    {
        // Arrange
        var invitation = Invitation.Create(_userId, status);

        // Act
        var result = invitation.Value.Accept();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invitation.IsFailure, Is.False);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(invitation.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(invitation.Value.Status, Is.EqualTo(InvitationStatus.Accepted));
        });
    }

    [Test, Category("Invitation")]
    [TestCase(InvitationStatus.Pending)]
    public void Success_Decline_Pending_Invitation(InvitationStatus status)
    {
        // Arrange
        var invitation = Invitation.Create(_userId, status);

        // Act
        var result = invitation.Value.Decline();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(invitation.IsFailure, Is.False);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(invitation.Value.GuestId, Is.EqualTo(_userId));
            Assert.That(invitation.Value.Status, Is.EqualTo(InvitationStatus.Rejected));
        });
    }
}