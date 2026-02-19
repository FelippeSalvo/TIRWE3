using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface ITreinoService
{
    Task<TreinoDto> CreateAsync(string usuarioId, CreateTreinoDto dto);
    Task<TreinoDto?> GetByIdAsync(string id, string usuarioId);
    Task<IEnumerable<TreinoDto>> GetByUsuarioIdAsync(string usuarioId);
    Task<TreinoDto?> UpdateAsync(string id, string usuarioId, UpdateTreinoDto dto);
    Task<bool> DeleteAsync(string id, string usuarioId);
}
