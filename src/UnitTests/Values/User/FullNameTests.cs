using VIAEventAssociation.Core.Domain.Entities.Values;

namespace Tests.Values.User;

[TestFixture]
public class FullNameTests
{
    [Test, Category("FullName")]
    [TestCase("Bob", "Bobsen")]
    public void Success_Create_FullName(string firstName, string lastName)
    {
        // Act
        var result = FullName.Create(firstName, lastName);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Value.ToString(), Is.Not.Null);
        });
    }

    [Test, Category("FullName")]
    [TestCase("john", "johnson", ExpectedResult = "John Johnson")]
    [TestCase("JOHN", "JOHNSON", ExpectedResult = "John Johnson")]
    [TestCase("jOhN", "JOhnSoN", ExpectedResult = "John Johnson")]
    [TestCase("jens", "andersen jens olsen", ExpectedResult = "Jens Andersen Jens Olsen")]
    [TestCase("JENS", "ANDERSEN JENS OLSEN", ExpectedResult = "Jens Andersen Jens Olsen")]
    [TestCase("jeNs", "AnderSEn jENs oLseN", ExpectedResult = "Jens Andersen Jens Olsen")]

    public string Success_Create_FullName_With_Correct_Cased_Letters(string firstName, string lastName)
    {
        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        Assert.That(result.IsFailure, Is.False);
        
        return result.Value.ToString();
    }
    
    [Test, Category("FullName")]
    [TestCase("Bo", "johnson")]
    [TestCase("Bo", "Bo")]
    [TestCase("Bobsen", "Bo")]
    [TestCase("Bob", "Bob")]
    [TestCase("ThisNameIsTwentyFiveChara", "Bob")]
    [TestCase("Bob", "ThisNameIsTwentyFourChara")]

    public void Success_Create_FullName_With_Valid_Length(string firstName, string lastName)
    {
        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        Assert.That(result.IsFailure, Is.False);
    }

    
    
    [Test, Category("FullName")]
    [TestCase("a", "Johnson")]
    [TestCase("", "Johnson")]
    [TestCase("John", "J")]
    [TestCase("John", "")]
    [TestCase("ThisNameIsTwentySixCharact", "Johnson")]
    [TestCase("ThisNameIsTwentySixCharact", "ThisNameIsTwentySevenChara")]
    [TestCase("ThisNameIsTwentySixCharact", "This Name Is TwentySeven Ch")]
    [TestCase("jgklfdsjilgjrliajiltjdsiirllsdijrlisejilrjselirjseilrjslr", "irojesjrioe jsiorjeois rjoiesjroiesjoirjesior ioesjriojesoi rjesoirj oiesroisejrojseoirj seoijrs")]
    public void Failure_Create_FullName_With_Invalid_Length(string firstName, string lastName)
    {
        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
    
    [Test, Category("FullName")]
    [TestCase("12345", "12345")]
    [TestCase("Hej1", "Johnson")]
    [TestCase("John", "J0hnson")]
    [TestCase("Jens#", "Jensen")]
    [TestCase("Jens ", "Jensen")]
    [TestCase("Jens Jens", "Jensen")]
    [TestCase("Jens", "Jen-sen")]
    [TestCase("Jens", "Jensen - Johnson")]
    public void Failure_Create_FullName_With_Invalid_Characters(string firstName, string lastName)
    {
        // Act
        var result = FullName.Create(firstName, lastName);

        // Assert
        Assert.That(result.IsFailure, Is.True);
    }
}