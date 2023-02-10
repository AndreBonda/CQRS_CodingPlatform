using CodingPlatform.Domain.Models;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class EmailTests
{
    [Test]
    public void Constructor_NullInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email(null));
    }

    [Test]
    public void Constructor_EmptyInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email(string.Empty));
    }

    [Test]
    public void Constructor_WhiteSpacesInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email(" "));
    }

    [Test]
    public void Constructor_WrongInputFormat_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Email("wrong_email"));
    }

    [Test]
    public void Constructor_InputValid_SetValue()
    {
        var email = new Email("valid_email@gmail.com");

        Assert.That(email.EmailValue, Is.EqualTo("valid_email@gmail.com"));
    }
}