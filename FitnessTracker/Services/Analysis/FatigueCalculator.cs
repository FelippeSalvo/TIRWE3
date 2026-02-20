using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Soma o FatorFadiga (1-5) dos exercícios no mesmo dia. Se ultrapassar o limite sugerido para o nível,
/// aplica penalidade (score 0-15).
/// Limites sugeridos por dia: Iniciante 12, Intermediário 18, Avançado 25.
/// </summary>
public static class FatigueCalculator
{
    private const int MaxScore = 15;

    public static (int Min, int Max) GetDailyFatigueLimit(string nivelUsuario)
    {
        var n = VolumeCalculator.NormalizeNivel(nivelUsuario);
        return n switch
        {
            "Iniciante" => (0, 12),
            "Intermediário" => (0, 18),
            "Avançado" => (0, 25),
            _ => (0, 18)
        };
    }

    /// <summary>
    /// Soma dos FatorFadiga de todos os exercícios do treino (considerando 1 por exercício no dia).
    /// </summary>
    public static int CalculateDailyFatigue(IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries)
    {
        return exerciseWithSeries.Sum(x => Math.Clamp(x.Ex.FatorFadiga, 1, 5));
    }

    /// <summary>
    /// Score 0-15: 15 = fadiga dentro do limite; penaliza se ultrapassar.
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(
        int dailyFatigue,
        string nivelUsuario)
    {
        var (_, maxAllowed) = GetDailyFatigueLimit(nivelUsuario);
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();

        if (dailyFatigue <= maxAllowed)
        {
            pontosFortes.Add($"Fadiga acumulada do dia ({dailyFatigue}) dentro do limite sugerido ({maxAllowed}).");
            return (MaxScore, pontosFortes, pontosFracos);
        }

        var excess = dailyFatigue - maxAllowed;
        var penalty = Math.Min(MaxScore, excess * 2);
        var score = Math.Max(0, MaxScore - penalty);
        pontosFracos.Add($"Fadiga acumulada alta ({dailyFatigue}). Limite sugerido para seu nível: {maxAllowed}. Considere reduzir exercícios muito desgastantes ou dividir o treino.");
        return (score, pontosFortes, pontosFracos);
    }
}
