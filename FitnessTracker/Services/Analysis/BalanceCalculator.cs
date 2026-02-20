using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Verifica equilíbrio: proporção Empurrar vs Puxar, anterior vs posterior, superior vs inferior.
/// Penaliza desequilíbrio grande. Score 0-20.
/// </summary>
public static class BalanceCalculator
{
    private const int MaxScore = 20;
    private static readonly HashSet<string> SuperiorGroups = new(StringComparer.OrdinalIgnoreCase)
        { "Peito", "Costas", "Ombro", "Tríceps", "Bíceps", "Trapézio", "Antebraço" };
    private static readonly HashSet<string> InferiorGroups = new(StringComparer.OrdinalIgnoreCase)
        { "Quadríceps", "Posterior", "Glúteos", "Panturrilha" };
    private static readonly HashSet<string> AnteriorGroups = new(StringComparer.OrdinalIgnoreCase)
        { "Peito", "Tríceps", "Quadríceps" };
    private static readonly HashSet<string> PosteriorGroups = new(StringComparer.OrdinalIgnoreCase)
        { "Costas", "Bíceps", "Posterior", "Glúteos" };

    public static (int pushCount, int pullCount) GetPushPullCounts(IEnumerable<Exercise> exercises)
    {
        int push = 0, pull = 0;
        foreach (var ex in exercises)
        {
            var t = (ex.TipoMovimento ?? "").Trim();
            if (t.Equals("Empurrar", StringComparison.OrdinalIgnoreCase)) push++;
            else if (t.Equals("Puxar", StringComparison.OrdinalIgnoreCase)) pull++;
        }
        return (push, pull);
    }

    public static (int superior, int inferior) GetUpperLowerCounts(IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries)
    {
        int sup = 0, inf = 0;
        foreach (var (ex, _) in exerciseWithSeries)
        {
            var main = (ex.GrupoMuscularPrincipal ?? "").Trim();
            if (SuperiorGroups.Contains(main)) sup++;
            else if (InferiorGroups.Contains(main)) inf++;
        }
        return (sup, inf);
    }

    public static (int anterior, int posterior) GetAnteriorPosteriorCounts(IEnumerable<Exercise> exercises)
    {
        int ant = 0, post = 0;
        foreach (var ex in exercises)
        {
            var main = (ex.GrupoMuscularPrincipal ?? "").Trim();
            if (AnteriorGroups.Contains(main)) ant++;
            else if (PosteriorGroups.Contains(main)) post++;
        }
        return (ant, post);
    }

    /// <summary>
    /// Score 0-20 baseado em três razões: push/pull (ideal ~1), superior/inferior (depende do dia), anterior/posterior (~1).
    /// Para um único treino (um dia), não exigimos que superior == inferior; avaliamos proporção push/pull e ant/post.
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(
        IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries)
    {
        var list = exerciseWithSeries.ToList();
        var exercises = list.Select(x => x.Ex).ToList();
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();

        var (push, pull) = GetPushPullCounts(exercises);
        var (ant, post) = GetAnteriorPosteriorCounts(exercises);
        var (sup, inf) = GetUpperLowerCounts(list);

        double scoreRatio = 1.0;
        if (push + pull > 0)
        {
            var ratio = push > 0 && pull > 0 ? Math.Min(push, pull) / (double)Math.Max(push, pull) : 0;
            if (ratio < 0.5)
            {
                pontosFracos.Add($"Desequilíbrio Empurrar/Puxar: {push} empurrar x {pull} puxar. Ideal ter proporção próxima de 1:1 na semana.");
                scoreRatio *= Math.Max(0.5, ratio + 0.5);
            }
            else
                pontosFortes.Add($"Proporção Empurrar/Puxar presente no treino.");
        }

        if (ant + post > 0)
        {
            var ratio = ant > 0 && post > 0 ? Math.Min(ant, post) / (double)Math.Max(ant, post) : 0;
            if (ratio < 0.4)
            {
                pontosFracos.Add($"Desequilíbrio anterior/posterior: {ant} anterior x {post} posterior.");
                scoreRatio *= Math.Max(0.6, ratio + 0.4);
            }
        }

        var rawScore = (int)Math.Round(scoreRatio * MaxScore);
        var score = Math.Clamp(rawScore, 0, MaxScore);
        return (score, pontosFortes, pontosFracos);
    }
}
