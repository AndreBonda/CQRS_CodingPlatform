namespace CodingPlatform.Domain.Models;

public class Tip
{
    public readonly string Description;
    public readonly byte Order;

    public Tip(string description, byte order)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(nameof(description));
        if (order <= 0) throw new ArgumentException(nameof(order));

        Description = description;
        Order = order;
    }
}