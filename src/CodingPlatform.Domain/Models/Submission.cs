using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Services;

namespace CodingPlatform.Domain.Models;

public class Submission : BaseEntity
{
    private const decimal _MAX_STARTING_SCORE = 5;
    private const decimal _MIN_STARTING_SCORE = 0;
    private const decimal _TIP_MALUS_PERCENTAGE = 0.1m;

    public byte TipsUsed { get; private set; }
    public DateTime? SubmitDate { get; private set; }
    public string Content { get; private set; }
    public decimal Score { get; private set; }
    public DateTime? EvaluateDate { get; set; }
    private readonly Challenge _challenge;
    private readonly Guid _playerId;
    private readonly IScoreCalculator _scoreCalculator;

    public Submission(Guid id, Guid playerId, Guid adminId, Challenge challenge, IScoreCalculator scoreCalculator)
        : base(id)
    {
        if (challenge == null) throw new ArgumentException(nameof(challenge));
        if (scoreCalculator == null) throw new ArgumentException(nameof(scoreCalculator));

        _playerId = playerId;
        _challenge = challenge;
        _scoreCalculator = scoreCalculator;
    }

    public int RemainingTips() => _challenge.TotalTips() - TipsUsed;

    public IEnumerable<Tip> GetAvailableTips()
    {
        foreach (var tip in _challenge.Tips.OrderBy(t => t.Order))
        {
            if (tip.Order > TipsUsed) yield break;
            yield return tip;
        }
    }

    public void RequestNewTip(Guid currentUserId)
    {
        if (!IsPlayer(currentUserId)) throw new BadRequestException("User is not the player of this challenge");

        if (IsSubmitted()) throw new BadRequestException("Challenge already submitted");

        if (!_challenge.IsActive()) throw new BadRequestException("Challenge is not active");

        if (RemainingTips() == 0) throw new BadRequestException("No more tips are available");

        TipsUsed++;
        UpdateDate = DateTime.UtcNow;
    }

    public void EndSubmission(Guid currentUserId, string content)
    {
        if (!IsPlayer(currentUserId)) throw new BadRequestException("User is not the player of this challenge");

        if (IsSubmitted()) throw new BadRequestException("Challenge already submitted");

        if (!_challenge.IsActive()) throw new BadRequestException("Challenge is not active");

        var now = DateTime.UtcNow;
        SubmitDate = now;
        UpdateDate = now;
        Content = content;
    }

    public bool IsSubmitted() => SubmitDate.HasValue;

    public bool IsEvaluated() => EvaluateDate.HasValue;

    public void Evaluate(Guid currentUserId, int initialScore)
    {
        if (!_challenge.IsAdmin(currentUserId)) throw new BadRequestException("User is not the challenge admin");

        if (!IsSubmitted()) throw new BadRequestException("You can evaluate only submitted submissioms");

        var now = DateTime.UtcNow;
        EvaluateDate = now;
        UpdateDate = now;
        Score = _scoreCalculator.CalculateScore(initialScore, TipsUsed);
    }

    private bool IsPlayer(Guid currentUser) => _playerId == currentUser;
}