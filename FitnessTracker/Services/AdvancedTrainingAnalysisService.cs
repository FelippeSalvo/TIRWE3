using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;
using FitnessTracker.Services.Analysis;

namespace FitnessTracker.Services;

/// <summary>
/// Orquestra a análise avançada: volume (25%), equilíbrio (20%), fadiga (15%), redundância (15%),
/// adequação ao nível (15%), frequência (10%). Score final 0-100, nunca negativo nem &gt; 100.
/// </summary>
public class AdvancedTrainingAnalysisService : IAdvancedTrainingAnalysisService
{
    private readonly ITreinoRepository _treinoRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public AdvancedTrainingAnalysisService(
        ITreinoRepository treinoRepository,
        IExerciseRepository exerciseRepository,
        IUsuarioRepository usuarioRepository)
    {
        _treinoRepository = treinoRepository;
        _exerciseRepository = exerciseRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<AdvancedAnalysisDto?> AnalyzeAsync(string idTreino, string usuarioId)
    {
        var treino = await _treinoRepository.GetByIdAsync(idTreino);
        if (treino == null || treino.UsuarioId != usuarioId)
            return null;

        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId);
        var nivelUsuario = usuario?.Nivel ?? "Iniciante";

        var exerciseIds = treino.ListaExercicios.Select(e => e.ExerciseId).ToList();
        if (exerciseIds.Count == 0)
        {
            return new AdvancedAnalysisDto
            {
                Score = 0,
                Classificacao = "Ruim",
                PontosFracos = new List<string> { "Treino sem exercícios." },
                Sugestoes = new List<string> { "Adicione exercícios ao treino usando o catálogo (GET /api/exercises)." }
            };
        }

        var exercises = await _exerciseRepository.GetByIdsAsync(exerciseIds);
        var exerciseById = exercises.ToDictionary(e => e.Id, StringComparer.OrdinalIgnoreCase);
        var exerciseWithSeries = treino.ListaExercicios
            .Where(item => exerciseById.ContainsKey(item.ExerciseId))
            .Select(item => (Ex: exerciseById[item.ExerciseId], Series: item.Series < 1 ? 3 : item.Series))
            .ToList();

        if (exerciseWithSeries.Count == 0)
        {
            return new AdvancedAnalysisDto
            {
                Score = 0,
                Classificacao = "Ruim",
                PontosFracos = new List<string> { "Nenhum exercício do treino encontrado no catálogo." },
                Sugestoes = new List<string> { "Verifique os IDs dos exercícios no treino." }
            };
        }

        var multi = VolumeCalculator.GetWeeklyFrequencyMultiplier(treino.DivisaoTreino);
        var weeklyPerMuscle = VolumeCalculator.CalculateWeeklySeriesPerMuscle(exerciseWithSeries, multi);

        var (volumeScore, volFortes, volFracos) = VolumeCalculator.CalculateScore(weeklyPerMuscle, nivelUsuario);
        var (balanceScore, balFortes, balFracos) = BalanceCalculator.CalculateScore(exerciseWithSeries);
        var dailyFatigue = FatigueCalculator.CalculateDailyFatigue(exerciseWithSeries);
        var (fatigueScore, fatFortes, fatFracos) = FatigueCalculator.CalculateScore(dailyFatigue, nivelUsuario);
        var (redundancyScore, redFortes, redFracos) = RedundancyDetector.CalculateScore(exerciseWithSeries);
        var (levelScore, levFortes, levFracos) = LevelAdequacyCalculator.CalculateScore(
            exerciseWithSeries.Select(x => x.Ex), nivelUsuario);
        var (freqScore, freqFortes, freqFracos) = FrequencyCalculator.CalculateScore(treino.DivisaoTreino);

        var totalScore = volumeScore + balanceScore + fatigueScore + redundancyScore + levelScore + freqScore;
        totalScore = Math.Clamp(totalScore, 0, 100);

        var classificacao = totalScore >= 80 ? "Excelente" : totalScore >= 60 ? "Bom" : totalScore >= 40 ? "Regular" : "Ruim";

        var pontosFortes = new List<string>();
        pontosFortes.AddRange(volFortes);
        pontosFortes.AddRange(balFortes);
        pontosFortes.AddRange(fatFortes);
        pontosFortes.AddRange(redFortes);
        pontosFortes.AddRange(levFortes);
        pontosFortes.AddRange(freqFortes);

        var pontosFracos = new List<string>();
        pontosFracos.AddRange(volFracos);
        pontosFracos.AddRange(balFracos);
        pontosFracos.AddRange(fatFracos);
        pontosFracos.AddRange(redFracos);
        pontosFracos.AddRange(levFracos);
        pontosFracos.AddRange(freqFracos);

        var sugestoes = new List<string>(pontosFracos);
        if (!MultiarticuladoCalculator.IsMajorityMultiarticulado(exerciseWithSeries.Select(x => x.Ex)))
            sugestoes.Add("A maioria dos exercícios deveria ser multiarticulada. Priorize movimentos compostos.");

        return new AdvancedAnalysisDto
        {
            Score = totalScore,
            Classificacao = classificacao,
            PontosFortes = pontosFortes,
            PontosFracos = pontosFracos,
            Sugestoes = sugestoes
        };
    }
}
