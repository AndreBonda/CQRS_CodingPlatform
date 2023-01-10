namespace CodingPlatform.Domain.Models;

public abstract class BaseEntity
{
    public readonly Guid Id;
    public DateTime CreateDate { get; protected set; }
    public DateTime UpdateDate { get; protected set; }

    public BaseEntity(Guid id)
    {
        Id = id;
    }

    public BaseEntity(Guid id, DateTime createDate)
    {
        Id = id;
        CreateDate = createDate;
        UpdateDate = createDate;
    }

    protected virtual void Validate()
    {
        if (CreateDate > UpdateDate) throw new ArgumentException("Create-date can't be greater than update-date");
    }
}