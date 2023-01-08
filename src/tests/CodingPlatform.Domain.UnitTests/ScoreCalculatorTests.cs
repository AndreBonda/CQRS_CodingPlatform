using CodingPlatform.Domain.Models.ValueObjects;
using CodingPlatform.Domain.Services;

namespace CodingPlatform.Domain.UnitTests;

[TestFixture]
public class ScoreCalculatorTests
{
    private class FakeScoreCalculator : ScoreCalculator
    {
        public void SetMinStartingScore(int minStartingScore) => _MIN_STARTING_SCORE = minStartingScore;

        public void SetMaxStartingScore(int maxStartingScore) => _MAX_STARTING_SCORE = maxStartingScore;

        public void SetTipMalusPercentage(decimal tipMalusPercentage) => _TIP_MALUS_PERCENTAGE = tipMalusPercentage;
    }

    [TestCase(-1)]
    [TestCase(6)]
    public void CalculateScore_InitialScoreOutOfRange_ThrowsArgumentException(int initialScore)
    {
        var fakeScoreCalculator = new FakeScoreCalculator();
        fakeScoreCalculator.SetMinStartingScore(0);
        fakeScoreCalculator.SetMaxStartingScore(5);

        Assert.Throws<ArgumentException>(() => fakeScoreCalculator.CalculateScore(initialScore, 0));
    }

    [Test]
    public void CalculateScore_TipsUserLessThanZero_ThrowsArgumentException()
    {
        var fakeScoreCalculator = new FakeScoreCalculator();
        fakeScoreCalculator.SetMinStartingScore(0);
        fakeScoreCalculator.SetMaxStartingScore(5);

        Assert.Throws<ArgumentException>(() => fakeScoreCalculator.CalculateScore(2, -1));
    }

    [TestCase(5,0,5)]
    [TestCase(3, 0, 3)]
    [TestCase(0, 0, 0)]
    [TestCase(4, 1, 3.5)]
    [TestCase(3, 2, 2)]
    [TestCase(1, 2, 0)]
    [TestCase(1, 8, 0)]
    public void CalculateScore_ValidInputs_TipMalus10Percentage_CorrectFinalScore(int initialScore, int tipsUsed, decimal finalScoreExpected)
    {
        var fakeScoreCalculator = new FakeScoreCalculator();
        fakeScoreCalculator.SetMinStartingScore(0);
        fakeScoreCalculator.SetMaxStartingScore(5);
        fakeScoreCalculator.SetTipMalusPercentage(0.1m); //10%

        decimal finalScore = fakeScoreCalculator.CalculateScore(initialScore, tipsUsed);

        Assert.That(finalScore, Is.EqualTo(finalScoreExpected));
    }

    [TestCase(5, 1, 2.5)]
    [TestCase(4, 1, 1.5)]
    [TestCase(1, 1, 0)]
    public void CalculateScore_ValidInputs_TipMalus50Percentage_CorrectFinalScore(int initialScore, int tipsUsed, decimal finalScoreExpected)
    {
        var fakeScoreCalculator = new FakeScoreCalculator();
        fakeScoreCalculator.SetMinStartingScore(0);
        fakeScoreCalculator.SetMaxStartingScore(5);
        fakeScoreCalculator.SetTipMalusPercentage(0.5m); //10%

        decimal finalScore = fakeScoreCalculator.CalculateScore(initialScore, tipsUsed);

        Assert.That(finalScore, Is.EqualTo(finalScoreExpected));
    }
}