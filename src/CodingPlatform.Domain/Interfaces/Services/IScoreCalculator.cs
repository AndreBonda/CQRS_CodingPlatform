namespace CodingPlatform.Domain.Interfaces.Services;

public interface IScoreCalculator
{
    /// <summary>
    /// Calculate final score applying malus points if tips are used.
    /// </summary>
    /// <param name="initialScore">Initial score set by admin</param>
    /// <param name="tipsUsed">Number of tips used by player</param>
    /// <returns></returns>
    decimal CalculateScore(int initialScore, int tipsUsed);
}