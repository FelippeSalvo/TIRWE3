using FitnessTracker.Models;

namespace FitnessTracker.Repositories;

public interface IWorkoutLogRepository
{
    Task<WorkoutLog?> GetByIdAsync(string id);
    Task<IEnumerable<WorkoutLog>> GetByUsuarioIdAsync(string usuarioId);
    Task<IEnumerable<WorkoutLog>> GetByUsuarioIdAsync(string usuarioId, DateTime dataInicio, DateTime dataFim);
    Task<WorkoutLog> CreateAsync(WorkoutLog log);
    Task<bool> DeleteAsync(string id);
}
