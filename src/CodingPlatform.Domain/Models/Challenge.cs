using System.ComponentModel.DataAnnotations;
using CodingPlatform.Domain.Exception;

namespace CodingPlatform.Domain.Models;

public class Challenge : BaseEntity
{
    private const int _MIN_HOURS_DURATION_CHALLENGE = 1;
    private const int _MAX_HOURS_DURATION_CHALLENGE = 3;

    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime EndDate { get; private set; }
    public IEnumerable<Tip> Tips { get; protected set; }
    public readonly Guid AdminId;

    public Challenge(Guid id, Guid adminId, string title, string description, int durationInHours, User amin, IEnumerable<string> tips = null)
    : base(id)
    {
        Validate(title, description, durationInHours, tips);

        Title = title;
        Description = description;
        EndDate = CreateDate.AddHours(durationInHours);
        Tips = InitializeTips(tips);
        AdminId = adminId;
    }

    public bool IsActive()
    {
        var now = DateTime.UtcNow;
        return CreateDate <= now && now <= EndDate;
    }

    public int TotalTips() => Tips.Count();

    public void UpdateChallenge(Guid currentUserId, string title, string description, int durationInHours, IEnumerable<string> tips = null)
    {
        if (IsActive()) throw new BadRequestException("Challenge is active");
        if (!IsAdmin(currentUserId)) throw new ForbiddenException("User is not the challenge admin");

        Validate(title, description, durationInHours, tips);

        Title = title;
        Description = description;
        EndDate = CreateDate.AddHours(durationInHours);
        Tips = InitializeTips(tips);
    }

    public void Validate(string title, string description, int durationInHours, IEnumerable<string> tips = null)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(nameof(title));

        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(nameof(description));

        if (durationInHours < _MIN_HOURS_DURATION_CHALLENGE || durationInHours > _MAX_HOURS_DURATION_CHALLENGE)
            throw new ArgumentException(nameof(durationInHours));

        if (tips.Any(t => string.IsNullOrWhiteSpace(t)))
            throw new ArgumentException(nameof(tips));
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