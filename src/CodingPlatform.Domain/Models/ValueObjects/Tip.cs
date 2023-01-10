namespace CodingPlatform.Domain.Models;

public class Tip : BaseEntity
{
    public readonly string Description;
    public readonly int Order;

    public Tip(string description, int order)
    : base(Guid.NewGuid(), DateTime.UtcNow)
    {
        Description = description;
        Order = order;

        Validate();
    }

    public Tip(Guid id, string description, int order, DateTime createDate, DateTime updateDate)
    : base(id)
    {
        Description = description;
        Order = order;
        CreateDate = createDate;
        UpdateDate = updateDate;

        Validate();
    }

    protected override void Validate()
    {
        base.Validate();

        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException(nameof(Description));
        if (Order <= 0) throw new ArgumentException(nameof(Order));
    }
}