using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

/// <summary>
/// Avalia a qualidade de um treino (template) com base em:
/// - Volume total (soma dos volumes dos exercícios)
/// - Frequência por grupo muscular (quantidade de grupos trabalhados)
/// - Distribuição equilibrada (nenhum grupo com mais de 50% dos exercícios)
/// - Quantidade adequada de séries (faixa recomendada por sessão)
/// Score final: média dos 4 critérios, cada um contribuindo até 25 pontos (total 0-100).
/// </summary>
public class TreinoAnalysisService : ITreinoAnalysisService
{
    private readonly ITreinoRepository _treinoRepository;

    // Faixas de referência para pontuação (ajustáveis)
    private const decimal VolumeMinimo = 3000;
    private const decimal VolumeIdealMin = 8000;
    private const decimal VolumeIdealMax = 25000;
    private const decimal VolumeMaximo = 40000;
    private const int SeriesMinimas = 8;
    private const int SeriesIdeaisMin = 12;
    private const int SeriesIdeaisMax = 25;
    private const int SeriesMaximas = 35;
    private const int GruposMinimos = 2;
    private const int GruposIdeais = 4;
    private const double MaxProporcaoGrupo = 0.5; // nenhum grupo deve ter > 50% dos exercícios

    public TreinoAnalysisService(ITreinoRepository treinoRepository)
    {
        _treinoRepository = treinoRepository;
    }

    public async Task<TreinoAnaliseDto?> AnalisarAsync(string idTreino, string usuarioId)
    {
        var treino = await _treinoRepository.GetByIdAsync(idTreino);
        if (treino == null || treino.UsuarioId != usuarioId)
            return null;

        var exercicios = treino.ListaExercicios;
        if (exercicios == null || exercicios.Count == 0)
        {
            return new TreinoAnaliseDto
            {
                Score = 0,
                Sugestoes = new List<string> { "Adicione ao menos um exercício ao treino." },
                LogicaScore = "O score é a média de 4 critérios (0-25 pts cada): volume total, frequência por grupo muscular, distribuição equilibrada e quantidade de séries. Treino vazio = 0."
            };
        }

        var volumeTotal = exercicios.Sum(e => e.VolumeTotal);
        var totalSeries = exercicios.Sum(e => e.Series);
        var grupos = exercicios.GroupBy(e => e.GrupoMuscular.Trim(), StringComparer.OrdinalIgnoreCase).ToList();
        var quantidadeGrupos = grupos.Count;
        var maxExerciciosEmUmGrupo = grupos.Max(g => g.Count());
        var proporcaoMaiorGrupo = (double)maxExerciciosEmUmGrupo / exercicios.Count;

        // 1) Volume (0-25): ideal entre VolumeIdealMin e VolumeIdealMax
        int pontosVolume = CalcularPontosVolume(volumeTotal);

        // 2) Frequência por grupo muscular (0-25): mais grupos = mais variedade
        int pontosFrequencia = CalcularPontosFrequenciaGrupos(quantidadeGrupos);

        // 3) Distribuição equilibrada (0-25): nenhum grupo com > 50% dos exercícios
        int pontosDistribuicao = proporcaoMaiorGrupo <= MaxProporcaoGrupo ? 25 : (int)Math.Max(0, 25 - (proporcaoMaiorGrupo - MaxProporcaoGrupo) * 50);

        // 4) Quantidade de séries (0-25): faixa ideal por sessão
        int pontosSeries = CalcularPontosSeries(totalSeries);

        int score = pontosVolume + pontosFrequencia + pontosDistribuicao + pontosSeries;
        var sugestoes = GerarSugestoes(volumeTotal, totalSeries, quantidadeGrupos, proporcaoMaiorGrupo, pontosVolume, pontosFrequencia, pontosDistribuicao, pontosSeries);

        var logicaScore = "Score = soma de 4 critérios (cada um 0-25 pts): " +
            "(1) Volume total: ideal 8.000-25.000; (2) Frequência: 4+ grupos = 25 pts; " +
            "(3) Distribuição: nenhum grupo com >50% dos exercícios; (4) Séries por sessão: ideal 12-25.";

        return new TreinoAnaliseDto
        {
            Score = Math.Min(100, score),
            Sugestoes = sugestoes,
            Detalhes = new TreinoAnaliseDetalheDto
            {
                PontosVolume = pontosVolume,
                PontosFrequenciaGrupos = pontosFrequencia,
                PontosDistribuicao = pontosDistribuicao,
                PontosSeries = pontosSeries,
                VolumeTotal = volumeTotal,
                TotalSeries = totalSeries,
                QuantidadeGruposMusculares = quantidadeGrupos
            },
            LogicaScore = logicaScore
        };
    }

    private static int CalcularPontosVolume(decimal volumeTotal)
    {
        if (volumeTotal < VolumeMinimo) return (int)(25 * (double)volumeTotal / (double)VolumeMinimo);
        if (volumeTotal >= VolumeIdealMin && volumeTotal <= VolumeIdealMax) return 25;
        if (volumeTotal <= VolumeMaximo)
        {
            if (volumeTotal < VolumeIdealMin)
                return 10 + (int)(15 * (double)(volumeTotal - VolumeMinimo) / (double)(VolumeIdealMin - VolumeMinimo));
            return 10 + (int)(15 * (double)(VolumeMaximo - volumeTotal) / (double)(VolumeMaximo - VolumeIdealMax));
        }
        return Math.Max(0, 25 - (int)((double)(volumeTotal - VolumeMaximo) / 1000));
    }

    private static int CalcularPontosFrequenciaGrupos(int quantidadeGrupos)
    {
        if (quantidadeGrupos < GruposMinimos) return quantidadeGrupos * 8;
        if (quantidadeGrupos >= GruposIdeais) return 25;
        return 10 + (int)(15 * (quantidadeGrupos - GruposMinimos) / (double)(GruposIdeais - GruposMinimos));
    }

    private static int CalcularPontosSeries(int totalSeries)
    {
        if (totalSeries < SeriesMinimas) return (int)(25 * (double)totalSeries / SeriesMinimas);
        if (totalSeries >= SeriesIdeaisMin && totalSeries <= SeriesIdeaisMax) return 25;
        if (totalSeries < SeriesIdeaisMin)
            return 10 + (int)(15 * (double)(totalSeries - SeriesMinimas) / (SeriesIdeaisMin - SeriesMinimas));
        if (totalSeries <= SeriesMaximas)
            return (int)(25 - 15 * (double)(totalSeries - SeriesIdeaisMax) / (SeriesMaximas - SeriesIdeaisMax));
        return Math.Max(0, 10 - (totalSeries - SeriesMaximas) / 2);
    }

    private static List<string> GerarSugestoes(decimal volumeTotal, int totalSeries, int quantidadeGrupos, double proporcaoMaiorGrupo, int pontosVolume, int pontosFrequencia, int pontosDistribuicao, int pontosSeries)
    {
        var sugestoes = new List<string>();
        if (pontosVolume < 20 && volumeTotal < VolumeIdealMin)
            sugestoes.Add("Aumente o volume total do treino (mais séries, repetições ou carga).");
        if (pontosVolume < 20 && volumeTotal > VolumeIdealMax)
            sugestoes.Add("O volume total está alto; considere reduzir para evitar overtraining.");
        if (pontosFrequencia < 20 && quantidadeGrupos < GruposIdeais)
            sugestoes.Add("Inclua mais grupos musculares para um treino mais completo.");
        if (pontosDistribuicao < 20 && proporcaoMaiorGrupo > MaxProporcaoGrupo)
            sugestoes.Add("Distribua melhor os exercícios: evite concentrar mais da metade em um único grupo.");
        if (pontosSeries < 20 && totalSeries < SeriesIdeaisMin)
            sugestoes.Add("Aumente a quantidade de séries para uma faixa adequada (ex.: 12-25 por sessão).");
        if (pontosSeries < 20 && totalSeries > SeriesIdeaisMax)
            sugestoes.Add("Muitas séries por sessão; considere dividir em mais dias ou reduzir.");
        if (sugestoes.Count == 0)
            sugestoes.Add("Treino dentro das recomendações. Mantenha a consistência!");
        return sugestoes;
    }
}
