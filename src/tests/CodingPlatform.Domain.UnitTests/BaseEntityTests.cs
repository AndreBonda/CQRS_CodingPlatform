using CodingPlatform.Domain.Models;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class BaseEntityTests
{
    private class FakeEntity : BaseEntity
    {
        public FakeEntity(Guid id) : base(id) { }

        public FakeEntity(Guid id, DateTime createDate) : base(id, createDate) { }
    }

    [Test]
    public void Constructor_ValidIdAndCrationDate_SetPropertiesCorrectly()
    {
        var guid = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var fakeEntity = new FakeEntity(guid, now);

        Assert.That(fakeEntity.Id, Is.EqualTo(guid));
        Assert.That(fakeEntity.CreateDate, Is.EqualTo(now));
        Assert.That(fakeEntity.UpdateDate, Is.EqualTo(now));
    }

    [Test]
    public void Constructor_ValidId_SetPropertiesCorrectly()
    {
        var guid = Guid.NewGuid();
        var now = DateTime.UtcNow;

        var fakeEntity = new FakeEntity(guid);

        Assert.That(fakeEntity.Id, Is.EqualTo(guid));
    }
}