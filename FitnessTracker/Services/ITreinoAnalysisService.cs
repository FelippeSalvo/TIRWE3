using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface ITreinoAnalysisService
{
    /// <summary>
    /// Avalia a qualidade do treino (template) e retorna score 0-100 e sugestões.
    /// </summary>
    Task<TreinoAnaliseDto?> AnalisarAsync(string idTreino, string usuarioId);
}
