using VIAEventAssociation.Core.Domain.Entities.User.Values;
using VIAEventAssociation.Core.Tools.OperationResult.Errors;

namespace Tests.Values.UserTests;

public class EmailTests
{
    [Test, Category("Email")]
    [TestCase("BOBS@via.dk")]
    public void Success_Create_Email(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Value, Is.EqualTo(email));
        });
    }
    
    [Test, Category("Email")]
    [TestCase("123456@via.dk")]
    [TestCase("546287@via.dk")]
    [TestCase("468297@via.dk")]
    [TestCase("111111@via.dk")]
    public void Success_Create_Email_With_Numbers(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Value, Is.EqualTo(email));
        });
    }
    
    [Test, Category("Email")]
    [TestCase("MANY@via.dk")]
    [TestCase("many@via.dk")]
    [TestCase("MOM@via.dk")]
    [TestCase("mom@via.dk")]
    public void Success_Create_Email_With_Letters(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Value, Is.EqualTo(email));
        });
    }
    
    
    [Test, Category("Email")]
    [TestCase("123456@VIA.DK")]
    [TestCase("546287@vIA.dK")]
    [TestCase("maNY@via.dK")]
    public void Success_Create_Email_With_Different_Cased_Letters(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value.Value, Is.EqualTo(email));
        });
    }
    
    
    [Test, Category("Email")]
    [TestCase(null)]
    [TestCase("")]
    public void Failure_Create_Email_That_Is_Empty(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailIsEmpty), Is.True);
        });    
    }
    
    [Test, Category("Email")]
    [TestCase("BOBS@via.k")]
    [TestCase("BOBS@via.dkk")]
    [TestCase("BOBS@via.com")]
    [TestCase("BOBS@via.")]
    [TestCase("BOBS@via.co")]
    [TestCase("BOBS@vi.co")]
    [TestCase("BOBS@.dk")]
    [TestCase("BOBSvia.dk")]
    public void Failure_Create_Email_With_Invalid_Ending(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailMustEndWith), Is.True);
        });        }
    
    [Test, Category("Email")]
    [TestCase("1@via.dk")]
    [TestCase("15@via.dk")]
    [TestCase("5783694@via.dk")]
    [TestCase("78514268@via.dk")]
    public void Failure_Create_Email_With_Numbers(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailMustStartWith), Is.True);
        });        
    }
    
    [Test, Category("Email")]
    [TestCase("as@via.dk")]
    [TestCase("asasdwae@via.dk")]
    [TestCase("looos@via.dk")]
    [TestCase("@via.dk")]
    [TestCase("@via.dk@via.dk")]
    [TestCase("MAN@via.dk@via.dk")]
    public void Failure_Create_Email_With_Letters(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailMustStartWith), Is.True);
        });
    }
    
    [Test, Category("Email")]
    [TestCase("AS12@via.dk")]
    [TestCase("3MAN@via.dk")]
    [TestCase("LO2@via.dk")]
    public void Failure_Create_Email_With_Letters_And_Numbers(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailMustStartWith), Is.True);
        });
    }
    
    [Test, Category("Email")]
    [TestCase("#@via.dk")]
    [TestCase("MAN#@via.dk")]
    [TestCase("LOL!@via.dk")]
    [TestCase("L-MN@via.dk")]
    public void Failure_Create_Email_With_Invalid_Characters(string email)
    {
        // Act
        var result = Email.Create(email);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Errors.Any(x => x.Code == ErrorCode.EmailWithInvalidCharacters), Is.True);
        });
    }
}