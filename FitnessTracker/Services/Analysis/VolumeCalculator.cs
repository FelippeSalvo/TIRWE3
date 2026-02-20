using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Calcula volume total semanal por grupo muscular e pontua (0-25) com base em faixas ideais por nível.
/// Faixas: Iniciante 10-12, Intermediário 12-18, Avançado 15-22 séries/semana por músculo.
/// Penaliza volume muito baixo ou excessivo.
/// </summary>
public static class VolumeCalculator
{
    private const int MaxScore = 25;

    /// <summary>
    /// Multiplicador de frequência semanal para este dia (quantas vezes este treino é feito por semana).
    /// </summary>
    public static int GetWeeklyFrequencyMultiplier(string divisaoTreino)
    {
        return divisaoTreino switch
        {
            "PushPullLegs" => 2,
            "UpperLower" => 2,
            "BroSplit" => 1,
            _ => 1
        };
    }

    /// <summary>
    /// Retorna (min, max) séries semanais ideais por grupo muscular para o nível.
    /// </summary>
    public static (int Min, int Max) GetIdealWeeklySeriesRange(string nivelUsuario)
    {
        var n = NormalizeNivel(nivelUsuario);
        return n switch
        {
            "Iniciante" => (10, 12),
            "Intermediário" => (12, 18),
            "Avançado" => (15, 22),
            _ => (10, 18)
        };
    }

    /// <summary>
    /// Calcula volume semanal por grupo muscular: para cada grupo, soma as séries dos exercícios
    /// que o têm como principal ou secundário e multiplica pelo fator de frequência semanal.
    /// </summary>
    public static Dictionary<string, int> CalculateWeeklySeriesPerMuscle(
        IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries,
        int weeklyMultiplier)
    {
        var dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var (ex, series) in exerciseWithSeries)
        {
            var contribution = series * weeklyMultiplier;
            AddToGroup(dict, ex.GrupoMuscularPrincipal, contribution);
            foreach (var sec in ex.GruposMuscularesSecundarios ?? new List<string>())
            {
                if (string.IsNullOrWhiteSpace(sec)) continue;
                AddToGroup(dict, sec, contribution / 2); // secundário conta metade
            }
        }
        return dict;
    }

    private static void AddToGroup(Dictionary<string, int> dict, string group, int value)
    {
        var key = group.Trim();
        if (string.IsNullOrEmpty(key)) return;
        dict.TryGetValue(key, out var current);
        dict[key] = current + value;
    }

    /// <summary>
    /// Pontua 0-25 com base na adequação do volume por grupo muscular ao nível do usuário.
    /// Média dos grupos: cada grupo dentro da faixa ideal = 100% do seu peso; fora = penalidade.
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(
        Dictionary<string, int> weeklySeriesPerMuscle,
        string nivelUsuario)
    {
        var (minIdeal, maxIdeal) = GetIdealWeeklySeriesRange(nivelUsuario);
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();
        if (weeklySeriesPerMuscle.Count == 0)
        {
            pontosFracos.Add("Nenhum grupo muscular identificado no treino.");
            return (0, pontosFortes, pontosFracos);
        }

        double sumRatio = 0;
        int count = 0;
        foreach (var (grupo, series) in weeklySeriesPerMuscle)
        {
            count++;
            double ratio;
            if (series < minIdeal)
            {
                ratio = (double)series / minIdeal;
                if (series < 5)
                    pontosFracos.Add($"Volume muito baixo para {grupo}: {series} séries/semana (mín. sugerido {minIdeal}).");
            }
            else if (series > maxIdeal)
            {
                var excess = series - maxIdeal;
                ratio = Math.Max(0, 1 - excess / (double)(maxIdeal + 10));
                pontosFracos.Add($"Volume alto para {grupo}: {series} séries/semana (máx. sugerido {maxIdeal}).");
            }
            else
            {
                ratio = 1;
                pontosFortes.Add($"Volume adequado para {grupo}: {series} séries/semana.");
            }
            sumRatio += ratio;
        }

        var avgRatio = count > 0 ? sumRatio / count : 0;
        var score = (int)Math.Round(avgRatio * MaxScore);
        score = Math.Clamp(score, 0, MaxScore);
        return (score, pontosFortes, pontosFracos);
    }

    public static string NormalizeNivel(string nivel)
    {
        if (string.IsNullOrWhiteSpace(nivel)) return "Iniciante";
        var n = nivel.Trim();
        if (n.Equals("Intermediario", StringComparison.OrdinalIgnoreCase) || n.Equals("Intermediário", StringComparison.OrdinalIgnoreCase))
            return "Intermediário";
        if (n.Equals("Avancado", StringComparison.OrdinalIgnoreCase) || n.Equals("Avançado", StringComparison.OrdinalIgnoreCase))
            return "Avançado";
        if (n.Equals("Iniciante", StringComparison.OrdinalIgnoreCase))
            return "Iniciante";
        return "Iniciante";
    }
}
