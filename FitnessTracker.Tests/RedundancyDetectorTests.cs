using FitnessTracker.Models;
using FitnessTracker.Services.Analysis;
using Xunit;

namespace FitnessTracker.Tests;

public class RedundancyDetectorTests
{
    private static Exercise Ex(string padrao, string grupo, string similaridade) => new()
    {
        PadraoMovimento = padrao,
        GrupoMuscularPrincipal = grupo,
        SimilaridadeGrupo = similaridade
    };

    [Fact]
    public void AreRedundant_SamePadraoAndGroup_ReturnsTrue()
    {
        var a = Ex("Supino horizontal", "Peito", "supino-h");
        var b = Ex("Supino horizontal", "Peito", "supino-h");
        Assert.True(RedundancyDetector.AreRedundant(a, b));
    }

    [Fact]
    public void AreRedundant_DifferentPadrao_ReturnsFalse()
    {
        var a = Ex("Supino horizontal", "Peito", "supino-h");
        var b = Ex("Supino inclinado", "Peito", "supino-h");
        Assert.False(RedundancyDetector.AreRedundant(a, b));
    }

    [Fact]
    public void AreRedundant_DifferentGroup_ReturnsFalse()
    {
        var a = Ex("Supino horizontal", "Peito", "supino-h");
        var b = Ex("Supino horizontal", "Costas", "supino-h");
        Assert.False(RedundancyDetector.AreRedundant(a, b));
    }

    [Fact]
    public void CountRedundantPairs_NoPairs_Returns0()
    {
        var list = new List<(Exercise Ex, int Series)>
        {
            (Ex("Supino", "Peito", "a"), 3),
            (Ex("Remada", "Costas", "b"), 3)
        };
        Assert.Equal(0, RedundancyDetector.CountRedundantPairs(list));
    }

    [Fact]
    public void CountRedundantPairs_OnePair_Returns1()
    {
        var list = new List<(Exercise Ex, int Series)>
        {
            (Ex("Supino horizontal", "Peito", "sup"), 3),
            (Ex("Supino horizontal", "Peito", "sup"), 4)
        };
        Assert.Equal(1, RedundancyDetector.CountRedundantPairs(list));
    }

    [Fact]
    public void CalculateScore_NoRedundancy_ReturnsMaxScore()
    {
        var list = new List<(Exercise Ex, int Series)>
        {
            (Ex("Supino", "Peito", "a"), 3),
            (Ex("Remada", "Costas", "b"), 3)
        };
        var (score, _, _) = RedundancyDetector.CalculateScore(list);
        Assert.Equal(15, score);
    }

    [Fact]
    public void CalculateScore_WithRedundancy_AppliesPenalty()
    {
        var list = new List<(Exercise Ex, int Series)>
        {
            (Ex("Supino horizontal", "Peito", "sup"), 3),
            (Ex("Supino horizontal", "Peito", "sup"), 3)
        };
        var (score, _, fracos) = RedundancyDetector.CalculateScore(list);
        Assert.True(score < 15);
        Assert.True(fracos.Any(f => f.Contains("redundantes")));
    }

    [Fact]
    public void CalculateScore_ScoreNeverNegative()
    {
        var list = new List<(Exercise Ex, int Series)>
        {
            (Ex("S", "P", "x"), 3),
            (Ex("S", "P", "x"), 3),
            (Ex("S", "P", "x"), 3)
        };
        var (score, _, _) = RedundancyDetector.CalculateScore(list);
        Assert.True(score >= 0);
    }
}
