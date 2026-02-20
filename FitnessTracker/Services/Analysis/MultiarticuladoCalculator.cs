using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Verifica proporção multiarticulado vs isolado. A maioria deve ser multiarticulado.
/// Contribui para o critério geral de equilíbrio ou pode ser um subcritério; aqui retornamos apenas a contagem.
/// (O score de equilíbrio já pode considerar isso; o spec diz "Maioria deve ser multiarticulado" - podemos
/// adicionar como parte do score de adequação ao nível ou um pequeno bônus/penalidade no score final.)
/// </summary>
public static class MultiarticuladoCalculator
{
    public static (int multi, int isolado) GetCounts(IEnumerable<Exercise> exercises)
    {
        int multi = 0, iso = 0;
        foreach (var ex in exercises)
        {
            if (ex.Multiarticulado) multi++;
            else iso++;
        }
        return (multi, iso);
    }

    /// <summary>
    /// Retorna true se a maioria dos exercícios é multiarticulado.
    /// </summary>
    public static bool IsMajorityMultiarticulado(IEnumerable<Exercise> exercises)
    {
        var (multi, iso) = GetCounts(exercises);
        return multi >= iso;
    }
}
