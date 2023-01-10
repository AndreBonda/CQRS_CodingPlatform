using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Models.ValueObjects;
using Moq;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class PasswordTests
{
    private Mock<IPasswordHasingProvider> _hashingProvider;

    [SetUp]
    public void SetUp()
    {
        // ===== Setup hashing password provider
        _hashingProvider = new Mock<IPasswordHasingProvider>();

        (byte[] Salt, byte[] Hash) tupletoReturn = new(new byte[1] { 0x20 }, new byte[1] { 0x21 });
        _hashingProvider
            .Setup(p => p.HashPassword(It.IsAny<string>()))
            .Returns(tupletoReturn);

        _hashingProvider
            .Setup(p => p.VerifyPassword(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
            .Returns(true);
    }

    [Test]
    public void Constructor_NullPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(null, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_EmptyPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(string.Empty, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_WhiteSpacesPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(" ", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_ShortPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aA*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoLowerCasePassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("AA*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoUpperCasePassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aa*1", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoNumericPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aa*A", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NoSpecialCharacterPassword_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("aaAA", _hashingProvider.Object));
    }

    [Test]
    public void Constructor_NullHashingProvider_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password("AAbb11**", null));
    }

    [Test]
    public void Constructor_ValidInputs_SetSaltAndHash()
    {
        var password = new Password("AAbb11**", _hashingProvider.Object);

        Assert.That(password.PasswordSalt, Is.EqualTo(new byte[1] { 0x20 }));
        Assert.That(password.PasswordHash, Is.EqualTo(new byte[1] { 0x21 }));
    }

    [Test]
    public void Constructor_PasswordSaltNull_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(null, new byte[1] { 0x21 }, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_PasswordSaltEmpty_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(new byte[0], new byte[1] { 0x21 }, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_PasswordHashNull_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(new byte[1] { 0x20 }, null, _hashingProvider.Object));
    }

    [Test]
    public void Constructor_PasswordHashEmpty_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(new byte[1] { 0x20 }, new byte[0], _hashingProvider.Object));
    }

    [Test]
    public void Constructor_HashingProviderNull_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Password(new byte[1] { 0x20 }, new byte[1] { 0x21 }, null));
    }

    [Test]
    public void Constructor_ValidInputs_SetSaltAndHash2()
    {
        var password = new Password(new byte[1] { 0x20 }, new byte[1] { 0x21 }, _hashingProvider.Object);

        Assert.That(password.PasswordSalt, Is.EqualTo(new byte[1] { 0x20 }));
        Assert.That(password.PasswordHash, Is.EqualTo(new byte[1] { 0x21 }));
    }

    [Test]
    public void VerifyPassword_CallHashingProviderCorrectly()
    {
        var password = new Password("AAbb11**", _hashingProvider.Object);

        password.IsPasswordCorrect("AAbb11**");

        _hashingProvider.Verify(p => p.VerifyPassword(
            It.Is<string>(password => password == "AAbb11**"),
            It.Is<byte[]>(salt => salt.SequenceEqual(new byte[1] { 0x20 })),
            It.Is<byte[]>(hash => hash.SequenceEqual(new byte[1] { 0x21 }))
        ));
    }
}