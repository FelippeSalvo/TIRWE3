using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Verifica se a quantidade de exercícios avançados é adequada ao nível do usuário.
/// Iniciante usando muitos exercícios avançados: penaliza. Score 0-15.
/// </summary>
public static class LevelAdequacyCalculator
{
    private const int MaxScore = 15;

    public static int CountByLevel(IEnumerable<Exercise> exercises, string level)
    {
        var n = VolumeCalculator.NormalizeNivel(level);
        return exercises.Count(e => string.Equals(NormalizeExerciseLevel(e.NivelDificuldade), n, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Penaliza se usuário iniciante tem mais de 1 exercício avançado; intermediário mais de 2 avançados.
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(
        IEnumerable<Exercise> exercises,
        string nivelUsuario)
    {
        var list = exercises.ToList();
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();
        var avançados = list.Count(e => string.Equals(NormalizeExerciseLevel(e.NivelDificuldade), "Avançado", StringComparison.OrdinalIgnoreCase));
        var nivel = VolumeCalculator.NormalizeNivel(nivelUsuario);

        int maxAvancados;
        switch (nivel)
        {
            case "Iniciante":
                maxAvancados = 1;
                break;
            case "Intermediário":
                maxAvancados = 2;
                break;
            default:
                maxAvancados = 10;
                break;
        }

        if (avançados <= maxAvancados)
        {
            pontosFortes.Add("Proporção de exercícios adequada ao seu nível.");
            return (MaxScore, pontosFortes, pontosFracos);
        }

        var excess = avançados - maxAvancados;
        var penalty = Math.Min(MaxScore, excess * 4);
        var score = Math.Max(0, MaxScore - penalty);
        pontosFracos.Add($"Muitos exercícios avançados ({avançados}) para nível {nivel}. Sugestão: até {maxAvancados} exercícios avançados.");
        return (score, pontosFortes, pontosFracos);
    }

    private static string NormalizeExerciseLevel(string? nivel)
    {
        if (string.IsNullOrWhiteSpace(nivel)) return "";
        var n = nivel.Trim();
        if (n.Equals("Avancado", StringComparison.OrdinalIgnoreCase) || n.Equals("Avançado", StringComparison.OrdinalIgnoreCase))
            return "Avançado";
        if (n.Equals("Intermediario", StringComparison.OrdinalIgnoreCase) || n.Equals("Intermediário", StringComparison.OrdinalIgnoreCase))
            return "Intermediário";
        if (n.Equals("Iniciante", StringComparison.OrdinalIgnoreCase))
            return "Iniciante";
        return n;
    }
}
