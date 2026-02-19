using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class WorkoutLogService : IWorkoutLogService
{
    private readonly IWorkoutLogRepository _repository;
    private readonly IMapper _mapper;

    public WorkoutLogService(IWorkoutLogRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WorkoutLogDto> RegistrarAsync(string usuarioId, RegistrarWorkoutLogDto dto)
    {
        var data = dto.Data ?? DateTime.UtcNow;
        var volume = dto.Carga * dto.Repeticoes;

        var log = new WorkoutLog
        {
            UsuarioId = usuarioId,
            ExercicioNome = dto.ExercicioNome.Trim(),
            Data = data,
            Carga = dto.Carga,
            Repeticoes = dto.Repeticoes,
            Volume = volume
        };

        await _repository.CreateAsync(log);
        return _mapper.Map<WorkoutLogDto>(log);
    }

    public async Task<WorkoutLogDto?> GetByIdAsync(string id, string usuarioId)
    {
        var log = await _repository.GetByIdAsync(id);
        if (log == null || log.UsuarioId != usuarioId) return null;
        return _mapper.Map<WorkoutLogDto>(log);
    }

    public async Task<IEnumerable<WorkoutLogDto>> GetHistoricoPorUsuarioAsync(string usuarioId)
    {
        var logs = await _repository.GetByUsuarioIdAsync(usuarioId);
        return _mapper.Map<IEnumerable<WorkoutLogDto>>(logs);
    }

    public async Task<IEnumerable<WorkoutLogDto>> GetHistoricoPorUsuarioAsync(string usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        var logs = await _repository.GetByUsuarioIdAsync(usuarioId, dataInicio, dataFim);
        return _mapper.Map<IEnumerable<WorkoutLogDto>>(logs);
    }
}
