using FitnessTracker.Models;
using FitnessTracker.Services.Analysis;
using Xunit;

namespace FitnessTracker.Tests;

public class FatigueCalculatorTests
{
    private static Exercise Ex(int fadiga) => new() { FatorFadiga = fadiga };

    [Fact]
    public void GetDailyFatigueLimit_Iniciante_Returns12()
    {
        var (_, max) = FatigueCalculator.GetDailyFatigueLimit("Iniciante");
        Assert.Equal(12, max);
    }

    [Fact]
    public void GetDailyFatigueLimit_Avancado_Returns25()
    {
        var (_, max) = FatigueCalculator.GetDailyFatigueLimit("Avançado");
        Assert.Equal(25, max);
    }

    [Fact]
    public void CalculateDailyFatigue_SumsFatorFadiga()
    {
        var data = new List<(Exercise Ex, int Series)>
        {
            (Ex(3), 4),
            (Ex(4), 3)
        };
        Assert.Equal(7, FatigueCalculator.CalculateDailyFatigue(data));
    }

    [Fact]
    public void CalculateDailyFatigue_ClampsFatorTo1_5()
    {
        var data = new List<(Exercise Ex, int Series)>
        {
            (Ex(0), 1),
            (Ex(10), 1)
        };
        var sum = FatigueCalculator.CalculateDailyFatigue(data);
        Assert.True(sum >= 2 && sum <= 10);
    }

    [Fact]
    public void CalculateScore_WithinLimit_ReturnsMaxScore()
    {
        var (score, _, _) = FatigueCalculator.CalculateScore(10, "Iniciante");
        Assert.Equal(15, score);
    }

    [Fact]
    public void CalculateScore_OverLimit_AppliesPenalty()
    {
        var (score, _, fracos) = FatigueCalculator.CalculateScore(20, "Iniciante");
        Assert.True(score < 15);
        Assert.True(fracos.Any(f => f.Contains("Fadiga")));
    }

    [Fact]
    public void CalculateScore_ScoreNeverNegative()
    {
        var (score, _, _) = FatigueCalculator.CalculateScore(100, "Iniciante");
        Assert.True(score >= 0);
    }

    [Fact]
    public void CalculateScore_ScoreNeverExceeds15()
    {
        var (score, _, _) = FatigueCalculator.CalculateScore(0, "Avançado");
        Assert.True(score <= 15);
    }
}
