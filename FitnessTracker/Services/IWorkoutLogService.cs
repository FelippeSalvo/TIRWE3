using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface IWorkoutLogService
{
    Task<WorkoutLogDto> RegistrarAsync(string usuarioId, RegistrarWorkoutLogDto dto);
    Task<WorkoutLogDto?> GetByIdAsync(string id, string usuarioId);
    Task<IEnumerable<WorkoutLogDto>> GetHistoricoPorUsuarioAsync(string usuarioId);
    Task<IEnumerable<WorkoutLogDto>> GetHistoricoPorUsuarioAsync(string usuarioId, DateTime dataInicio, DateTime dataFim);
}
