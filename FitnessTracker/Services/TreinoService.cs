using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class TreinoService : ITreinoService
{
    private readonly ITreinoRepository _treinoRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public TreinoService(ITreinoRepository treinoRepository, IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _treinoRepository = treinoRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    private static List<TreinoExercicioItem> MapExercicioItems(IEnumerable<TreinoExercicioItemInputDto> inputs)
    {
        return inputs.Select(i => new TreinoExercicioItem
        {
            ExerciseId = i.ExerciseId.Trim(),
            Series = i.Series < 1 ? 3 : i.Series
        }).ToList();
    }

    public async Task<TreinoDto?> CreateAsync(string usuarioId, CreateTreinoDto dto)
    {
        var exerciseIds = dto.ListaExercicios.Select(e => e.ExerciseId.Trim()).Distinct().ToList();
        var existing = await _exerciseRepository.GetByIdsAsync(exerciseIds);
        var existingIds = existing.Select(e => e.Id).ToHashSet();
        var invalid = exerciseIds.Where(id => !existingIds.Contains(id)).ToList();
        if (invalid.Count > 0)
            return null; // caller should check and return BadRequest

        var treino = new Treino
        {
            UsuarioId = usuarioId,
            Nome = dto.Nome.Trim(),
            GrupoMuscularFoco = dto.GrupoMuscularFoco.Trim(),
            DivisaoTreino = dto.DivisaoTreino.Trim(),
            ListaExercicios = MapExercicioItems(dto.ListaExercicios),
            DataCriacao = DateTime.UtcNow
        };

        await _treinoRepository.CreateAsync(treino);
        return _mapper.Map<TreinoDto>(treino);
    }

    public async Task<TreinoDto?> GetByIdAsync(string id, string usuarioId)
    {
        var treino = await _treinoRepository.GetByIdAsync(id);
        if (treino == null || treino.UsuarioId != usuarioId)
            return null;
        return _mapper.Map<TreinoDto>(treino);
    }

    public async Task<IEnumerable<TreinoDto>> GetByUsuarioIdAsync(string usuarioId)
    {
        var treinos = await _treinoRepository.GetByUsuarioIdAsync(usuarioId);
        return _mapper.Map<IEnumerable<TreinoDto>>(treinos);
    }

    public async Task<TreinoUpdateResult> UpdateAsync(string id, string usuarioId, UpdateTreinoDto dto)
    {
        var treino = await _treinoRepository.GetByIdAsync(id);
        if (treino == null || treino.UsuarioId != usuarioId)
            return new TreinoUpdateResult { Error = "NotFound" };

        var exerciseIds = dto.ListaExercicios.Select(e => e.ExerciseId.Trim()).Distinct().ToList();
        var existing = await _exerciseRepository.GetByIdsAsync(exerciseIds);
        var existingIds = existing.Select(e => e.Id).ToHashSet();
        var invalid = exerciseIds.Where(eid => !existingIds.Contains(eid)).ToList();
        if (invalid.Count > 0)
            return new TreinoUpdateResult { Error = "InvalidExerciseIds" };

        treino.Nome = dto.Nome.Trim();
        treino.GrupoMuscularFoco = dto.GrupoMuscularFoco.Trim();
        treino.DivisaoTreino = dto.DivisaoTreino.Trim();
        treino.ListaExercicios = MapExercicioItems(dto.ListaExercicios);

        await _treinoRepository.UpdateAsync(treino);
        return new TreinoUpdateResult { Treino = _mapper.Map<TreinoDto>(treino) };
    }

    public async Task<bool> DeleteAsync(string id, string usuarioId)
    {
        var treino = await _treinoRepository.GetByIdAsync(id);
        if (treino == null || treino.UsuarioId != usuarioId)
            return false;
        return await _treinoRepository.DeleteAsync(id);
    }
}
