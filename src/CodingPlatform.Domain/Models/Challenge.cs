using CodingPlatform.Domain.Exception;

namespace CodingPlatform.Domain.Models;

public class Challenge : BaseEntity
{
    public const int _MIN_HOURS_DURATION_CHALLENGE = 1;
    public const int _MAX_HOURS_DURATION_CHALLENGE = 3;

    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime EndDate { get; private set; }
    public IEnumerable<Tip> Tips { get; protected set; }
    public readonly Guid AdminId;

    public Challenge(Guid id, Guid adminId, string title, string description, DateTime endDate, IEnumerable<string> tips = null)
    : base(id, DateTime.UtcNow)
    {
        AdminId = adminId;
        Title = title;
        Description = description;
        EndDate = endDate;
        Tips = InitializeTips(tips);

        Validate();
    }

    public Challenge(Guid id, Guid adminId, string title, string description, DateTime endDate, DateTime createDate, DateTime updateDate, IEnumerable<Tip> tips = null)
    : base(id)
    {
        AdminId = adminId;
        Title = title;
        Description = description;
        EndDate = endDate;
        Tips = tips;
        CreateDate = createDate;
        UpdateDate = updateDate;

        Validate();
    }

    public bool IsActive()
    {
        var now = DateTime.UtcNow;
        return CreateDate <= now && now <= EndDate;
    }

    public int TotalTips() => Tips.Count();

    public void UpdateChallenge(Guid currentUserId, string title, string description, DateTime endDate, IEnumerable<string> tips = null)
    {
        if (IsActive()) throw new BadRequestException("Challenge is active");
        if (!IsAdmin(currentUserId)) throw new ForbiddenException("User is not the challenge admin");

        Title = title;
        Description = description;
        EndDate = EndDate;
        Tips = InitializeTips(tips);

        Validate();
    }

    protected override void Validate()
    {
        base.Validate();

        if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException(nameof(Title));
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException(nameof(Description));
        if (CreateDate > EndDate) throw new ArgumentException("Create-date can't be greater than end-date");

        if (Tips.Any())
        {
            int tipsNumber = Tips.Count();
            var expectedTipOrdering = Enumerable.Range(1, tipsNumber).ToArray();
            var tipOrdering = Tips.OrderBy(t => t.Order).Select(t => t.Order).ToArray();

            if (!expectedTipOrdering.SequenceEqual(tipOrdering))
                throw new ArgumentException("Invalid order sequence");
        }
    }

    public bool IsAdmin(Guid currentUserId) => this.AdminId == currentUserId;

    private IEnumerable<Tip> InitializeTips(IEnumerable<string> tips)
    {
        if (tips == null) return Enumerable.Empty<Tip>();

        var newTips = new List<Tip>();
        byte count = 1;

        foreach (string t in tips)
        {
            newTips.Add(new Tip(t, count));
            count++;
        }

        return newTips;
    }
}