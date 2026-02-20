using FitnessTracker.Models;

namespace FitnessTracker.Services.Analysis;

/// <summary>
/// Verifica se cada grupo muscular é treinado ao menos 2x por semana (exceto Bro Split, que é 1x por grupo).
/// Para um único treino (um dia), inferimos: com a divisão escolhida, os grupos deste treino são
/// treinados N vezes por semana (PushPullLegs/UpperLower = 2, BroSplit = 1).
/// Score 0-10: 10 = frequência ok; penaliza Bro Split para grupos que precisariam de 2x (ou seja, no contexto
/// de análise de um dia só, assumimos que Bro Split = 1x/semana por grupo, então penalizamos um pouco).
/// </summary>
public static class FrequencyCalculator
{
    private const int MaxScore = 10;

    /// <summary>
    /// Para o tipo de divisão, quantas vezes por semana este dia (e portanto os grupos deste treino) é treinado.
    /// </summary>
    public static int GetTimesPerWeekForThisDay(string divisaoTreino)
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
    /// Score 0-10: se divisão é PPL ou UpperLower, frequência 2x = ideal (10). Bro Split 1x = penalidade leve (7).
    /// Personalizado 1x = penalidade.
    /// </summary>
    public static (int Score, List<string> PontosFortes, List<string> PontosFracos) CalculateScore(string divisaoTreino)
    {
        var pontosFortes = new List<string>();
        var pontosFracos = new List<string>();
        var times = GetTimesPerWeekForThisDay(divisaoTreino);

        if (times >= 2)
        {
            pontosFortes.Add("Frequência semanal adequada (2x por grupo nesta divisão).");
            return (MaxScore, pontosFortes, pontosFracos);
        }

        if (divisaoTreino.Equals("BroSplit", StringComparison.OrdinalIgnoreCase))
        {
            pontosFracos.Add("No Bro Split cada grupo é treinado 1x/semana. Para maior estímulo, considere PPL ou Upper/Lower (2x por grupo).");
            return (7, pontosFortes, pontosFracos);
        }

        pontosFracos.Add("Divisão personalizada com 1x/semana por dia. Para melhores resultados, treine cada grupo ao menos 2x/semana.");
        return (5, pontosFortes, pontosFracos);
    }
}
