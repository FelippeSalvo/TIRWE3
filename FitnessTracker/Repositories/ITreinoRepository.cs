using FitnessTracker.Models;

namespace FitnessTracker.Repositories;

public interface ITreinoRepository
{
    Task<Treino?> GetByIdAsync(string id);
    Task<IEnumerable<Treino>> GetByUsuarioIdAsync(string usuarioId);
    Task<Treino> CreateAsync(Treino treino);
    Task<Treino> UpdateAsync(Treino treino);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
