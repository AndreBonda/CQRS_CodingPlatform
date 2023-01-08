using CodingPlatform.Domain.Interfaces.Services;

namespace CodingPlatform.Domain.Services;

public class ScoreCalculator : IScoreCalculator
{
    protected int _MIN_STARTING_SCORE = 0, _MAX_STARTING_SCORE = 5;
    protected decimal _TIP_MALUS_PERCENTAGE = 0.1m;

    public decimal CalculateScore(int initialScore, int tipsUsed)
    {
        if (initialScore < _MIN_STARTING_SCORE || initialScore > _MAX_STARTING_SCORE)
            throw new ArgumentException(nameof(initialScore));

        if (tipsUsed < 0)
            throw new ArgumentException(nameof(tipsUsed));

        decimal singleMalusValue = _MAX_STARTING_SCORE * _TIP_MALUS_PERCENTAGE;
        return Math.Max(0, initialScore - singleMalusValue * tipsUsed); // Using Math.Max to avoid negative final-score
    }
}