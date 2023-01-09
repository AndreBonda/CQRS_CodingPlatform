using CodingPlatform.Domain.Models;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class BaseEntityTests
{
    private class FakeEntity : BaseEntity
    {
        public FakeEntity(Guid id) : base(id)
        {
        }
    }

    [Test]
    public void Constructor_ValidInputGuid_SetPropertiesCorrectly()
    {
        var guid = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var fakeEntity = new FakeEntity(guid);

        Assert.That(fakeEntity.Id, Is.EqualTo(guid));
        Assert.That(fakeEntity.CreateDate, Is.GreaterThan(now));
        Assert.That(fakeEntity.UpdateDate, Is.GreaterThan(now));
        Assert.That(fakeEntity.CreateDate, Is.EqualTo(fakeEntity.UpdateDate));
    }
}