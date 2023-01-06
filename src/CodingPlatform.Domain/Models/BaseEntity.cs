namespace CodingPlatform.Domain.Models;

public abstract class BaseEntity
{
    public readonly Guid Id;
    public readonly DateTime CreateDate;
    public DateTime UpdateDate { get; protected set; }

    public BaseEntity(Guid id)
    {        
        Id = id;
        CreateDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
}