using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface IAdvancedTrainingAnalysisService
{
    /// <summary>
    /// Análise avançada do treino: volume, equilíbrio, fadiga, redundância, adequação ao nível, frequência.
    /// Retorna score 0-100, classificação, pontos fortes/fracos e sugestões.
    /// </summary>
    Task<AdvancedAnalysisDto?> AnalyzeAsync(string idTreino, string usuarioId);
}
