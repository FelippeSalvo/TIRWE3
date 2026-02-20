using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Detecta redundância de estímulo: exercícios com mesmo PadraoMovimento, mesmo GrupoMuscularPrincipal
/// e mesma SimilaridadeGrupo são considerados redundantes. Aplica penalidade (reduz score de 0-15).
/// </summary>
public static class RedundancyDetector
{
    private const int MaxScore = 15;

    /// <summary>
    /// Conta pares redundantes no conjunto (exercício, séries). Dois exercícios são redundantes se
    /// têm mesmo PadraoMovimento, mesmo GrupoMuscularPrincipal e mesma SimilaridadeGrupo (não vazia).
    /// </summary>
    public static int CountRedundantPairs(IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries)
    {
        var list = exerciseWithSeries.ToList();
        int pairs = 0;
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (AreRedundant(list[i].Ex, list[j].Ex))
                    pairs++;
            }
        }
        return pairs;
    }

    public static bool AreRedundant(Exercise a, Exercise b)
    {
        if (string.IsNullOrWhiteSpace(a.PadraoMovimento) || string.IsNullOrWhiteSpace(b.PadraoMovimento))
            return false;
        if (!string.Equals(a.PadraoMovimento.Trim(), b.PadraoMovimento.Trim(), StringComparison.OrdinalIgnoreCase))
            return false;
        if (!string.Equals(a.GrupoMuscularPrincipal.Trim(), b.GrupoMuscularPrincipal.Trim(), StringComparison.OrdinalIgnoreCase))
            return false;
        if (string.IsNullOrWhiteSpace(a.SimilaridadeGrupo) || string.IsNullOrWhiteSpace(b.SimilaridadeGrupo))
            return true; // mesmo padrão e grupo já indica redundância
        return string.Equals(a.SimilaridadeGrupo.Trim(), b.SimilaridadeGrupo.Trim(), StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Calcula score 0-15: 15 = sem redundância; reduz 3 pontos por par redundante (mínimo 0).
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(
        IEnumerable<(Exercise Ex, int Series)> exerciseWithSeries)
    {
        var list = exerciseWithSeries.ToList();
        var redundantPairs = CountRedundantPairs(list);
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();

        if (redundantPairs == 0)
        {
            pontosFortes.Add("Nenhum exercício redundante detectado.");
            return (MaxScore, pontosFortes, pontosFracos);
        }

        var penalty = Math.Min(MaxScore, redundantPairs * 3);
        var score = Math.Max(0, MaxScore - penalty);
        pontosFracos.Add($"Foram detectados {redundantPairs} par(es) de exercícios redundantes (mesmo padrão/grupo/similaridade). Diversifique os padrões de movimento.");
        return (score, pontosFortes, pontosFracos);
    }
}
