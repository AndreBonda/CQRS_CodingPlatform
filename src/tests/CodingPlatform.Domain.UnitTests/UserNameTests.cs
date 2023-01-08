using CodingPlatform.Domain.Models.ValueObjects;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class UserNameTests
{
    [TestCase(null)]
    [TestCase(" ")]
    public void Constructor_NullOrWhiteSpaceInput_ThrowsArgumentException(string username)
    {
        Assert.Throws<ArgumentException>(() => new Email(username));
    }
}