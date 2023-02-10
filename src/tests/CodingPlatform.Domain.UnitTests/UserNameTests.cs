using CodingPlatform.Domain.Models;

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