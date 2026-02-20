using FitnessTracker.Models;
using FitnessTracker.Services.Analysis;
using Xunit;

namespace FitnessTracker.Tests;

public class VolumeCalculatorTests
{
    [Fact]
    public void GetWeeklyFrequencyMultiplier_PushPullLegs_Returns2()
    {
        Assert.Equal(2, VolumeCalculator.GetWeeklyFrequencyMultiplier("PushPullLegs"));
    }

    [Fact]
    public void GetWeeklyFrequencyMultiplier_UpperLower_Returns2()
    {
        Assert.Equal(2, VolumeCalculator.GetWeeklyFrequencyMultiplier("UpperLower"));
    }

    [Fact]
    public void GetWeeklyFrequencyMultiplier_BroSplit_Returns1()
    {
        Assert.Equal(1, VolumeCalculator.GetWeeklyFrequencyMultiplier("BroSplit"));
    }

    [Fact]
    public void GetWeeklyFrequencyMultiplier_Personalizado_Returns1()
    {
        Assert.Equal(1, VolumeCalculator.GetWeeklyFrequencyMultiplier("Personalizado"));
    }

    [Fact]
    public void GetIdealWeeklySeriesRange_Iniciante_Returns10_12()
    {
        var (min, max) = VolumeCalculator.GetIdealWeeklySeriesRange("Iniciante");
        Assert.Equal(10, min);
        Assert.Equal(12, max);
    }

    [Fact]
    public void GetIdealWeeklySeriesRange_Intermediario_Returns12_18()
    {
        var (min, max) = VolumeCalculator.GetIdealWeeklySeriesRange("Intermediário");
        Assert.Equal(12, min);
        Assert.Equal(18, max);
    }

    [Fact]
    public void GetIdealWeeklySeriesRange_Avancado_Returns15_22()
    {
        var (min, max) = VolumeCalculator.GetIdealWeeklySeriesRange("Avançado");
        Assert.Equal(15, min);
        Assert.Equal(22, max);
    }

    [Fact]
    public void CalculateWeeklySeriesPerMuscle_SingleExercise_AddsToPrincipalGroup()
    {
        var ex = new Exercise { GrupoMuscularPrincipal = "Peito", GruposMuscularesSecundarios = new List<string>() };
        var data = new List<(Exercise Ex, int Series)> { (ex, 4) };
        var result = VolumeCalculator.CalculateWeeklySeriesPerMuscle(data, 2);
        Assert.True(result.ContainsKey("Peito"));
        Assert.Equal(8, result["Peito"]); // 4 series * 2 weekly
    }

    [Fact]
    public void CalculateWeeklySeriesPerMuscle_WithSecondary_AddsHalfToSecondary()
    {
        var ex = new Exercise
        {
            GrupoMuscularPrincipal = "Peito",
            GruposMuscularesSecundarios = new List<string> { "Tríceps" }
        };
        var data = new List<(Exercise Ex, int Series)> { (ex, 4) };
        var result = VolumeCalculator.CalculateWeeklySeriesPerMuscle(data, 2);
        Assert.Equal(8, result["Peito"]);
        Assert.Equal(4, result["Tríceps"]); // 8/2
    }

    [Fact]
    public void CalculateScore_EmptyMuscles_ReturnsZeroAndWeakPoint()
    {
        var (score, fortes, fracos) = VolumeCalculator.CalculateScore(new Dictionary<string, int>(), "Iniciante");
        Assert.Equal(0, score);
        Assert.True(fracos.Any(f => f.Contains("Nenhum grupo")));
    }

    [Fact]
    public void CalculateScore_VolumeWithinRange_ReturnsMaxScore()
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Peito"] = 11 };
        var (score, _, _) = VolumeCalculator.CalculateScore(dict, "Iniciante");
        Assert.Equal(25, score);
    }

    [Fact]
    public void CalculateScore_VolumeTooLow_ReturnsLessThanMax()
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Peito"] = 3 };
        var (score, _, fracos) = VolumeCalculator.CalculateScore(dict, "Iniciante");
        Assert.True(score < 25);
        Assert.True(fracos.Any(f => f.Contains("muito baixo")));
    }

    [Fact]
    public void CalculateScore_ScoreNeverNegative()
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["Peito"] = 0 };
        var (score, _, _) = VolumeCalculator.CalculateScore(dict, "Iniciante");
        Assert.True(score >= 0);
    }

    [Fact]
    public void CalculateScore_ScoreNeverExceeds25()
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Peito"] = 12,
            ["Costas"] = 12
        };
        var (score, _, _) = VolumeCalculator.CalculateScore(dict, "Iniciante");
        Assert.True(score <= 25);
    }
}
