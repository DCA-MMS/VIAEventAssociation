using VIAEventAssociation.Core.Domain.Aggregates.Users;
using VIAEventAssociation.Core.Domain.Aggregates.Users.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;
using VIAEventAssociation.Core.Tools.OperationResult.Errors.User;

namespace Tests.Features.Event.UC10___Create_user;

public class CreateUserAggregateTests
{
    // # S1
    [Test]
    public void Anon_Registers_With_Valid_Data_Should_Succeed()
    {
        // Arrange
        var fullName = FullName.Create("bob", "bobsen");
        var email = Email.Create("bob@via.dk");
        var newUser = User.Create(fullName, email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.False);
            Assert.That(email.IsFailure, Is.False);
            Assert.That(newUser.Value.FullName.ToString(), Is.EqualTo("Bob Bobsen"));
            Assert.That(newUser.Value.Email, Is.EqualTo(email.Value));
        });
    }
    
    // # F1
    [Test]
    public void Anon_Registers_With_Email_That_Does_Not_End_With_Via_Dk_Should_Fail()
    {
        // Arrange
        var fullName = FullName.Create("bob", "bobsen");
        var email = Email.Create("bob@notvia.dk");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.False);
            Assert.That(email.IsFailure, Is.True);
            Assert.That(email.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserEmailError.EmailMustEndWith().Code));
        });
    }
    
    // # F2
    [Test]
    [TestCase("MoreThanFourLetters@via.dk")]
    [TestCase("1234567@via.dk")] // 7 digits
    public void Anon_Registers_With_Invalid_Email_Format_Should_Fail(string testEmail)
    {
        // Arrange
        var fullName = FullName.Create("bob", "bobsen");
        var email = Email.Create(testEmail);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.False);
            Assert.That(email.IsFailure, Is.True);
            Assert.That(email.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserEmailError.EmailMustStartWith().Code));
        });
    }
    
    // # F3
    [Test]
    public void Anon_Registers_With_Invalid_First_Name_Should_Fail()
    {
        // Arrange
        var fullName = FullName.Create("b", "bobsen");
        var email = Email.Create("bob@via.dk");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.True);
            Assert.That(fullName.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.FirstNameIsTooShort().Code));
            Assert.That(email.IsFailure, Is.False);
        });
    }
    
    // # F4
    [Test]
    public void Anon_Registers_With_Invalid_Last_Name_Should_Fail()
    {
        // Arrange
        var fullName = FullName.Create("bob", "b");
        var email = Email.Create("bob@via.dk");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.True);
            Assert.That(fullName.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.LastNameIsTooShort().Code));
            Assert.That(email.IsFailure, Is.False);
        });
    }
    
    // # F5 How do I test that the email is already registered without a database?
    
    // # F6
    [Test]
    public void Anon_Registers_With_Numbers_In_FullName_Should_Fail()
    {
        // Arrange
        var fullName = FullName.Create("bob1", "bobsen2");
        var email = Email.Create("bob@via.dk");
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.True);
            Assert.That(fullName.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.FirstNameIsInvalid().Code));
            Assert.That(fullName.Errors, Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.LastNameIsInvalid().Code));
            Assert.That(email.IsFailure, Is.False);
        });
    }
    
    // # F7
    [Test]
    public void Anon_Registers_With_Special_Characters_In_FullName_Should_Fail()
    {
        // Arrange
        var fullName = FullName.Create("bob!", "bobsen?");
        var email = Email.Create("bob@via.dk");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(fullName.IsFailure, Is.True);
            Assert.That(fullName.Errors,
                Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.FirstNameIsInvalid().Code));
            Assert.That(fullName.Errors,
                Has.Exactly(1).Matches<Error>(x => x.Code == UserFullNameError.LastNameIsInvalid().Code));
            Assert.That(email.IsFailure, Is.False);
        });
    }
}