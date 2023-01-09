using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.Models.ValueObjects;
using Moq;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class UserTests
{
    private Mock<Email> _email;
    private Mock<Username> _username;
    private Mock<Password> _password;

    [SetUp]
    public void SetUp()
    {
        _email = new Mock<Email>();
        _username = new Mock<Username>();
        _password = new Mock<Password>();
    }

    [Test]
    public void Constructor_NullEmail_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), null, _username.Object, _password.Object));
    }

    [Test]
    public void Constructor_NullUsername_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), _email.Object, null, _password.Object));
    }

    [Test]
    public void Constructor_NullPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), _email.Object, _username.Object, null));
    }

    [Test]
    public void Constructor_ValidInputs_SetPropertiesCorrectly()
    {
        _email.Setup(e => e.EmailValue).Returns("valid_email");
        _username.Setup(u => u.UsernameValue).Returns("valid_username");
        _password.Setup(p => p.PasswordSalt).Returns(new byte[1] { 0x20 });
        _password.Setup(p => p.PasswordHash).Returns(new byte[1] { 0x21 });

        var user = new User(Guid.NewGuid(), _email.Object, _username.Object, _password.Object);

        Assert.That(user.Email, Is.EqualTo("valid_email"));
        Assert.That(user.Username, Is.EqualTo("valid_username"));
        Assert.That(user.PasswordSalt, Is.EquivalentTo(new byte[1] { 0x20 }));
        Assert.That(user.PasswordHash, Is.EquivalentTo(new byte[1] { 0x21 }));
    }
}