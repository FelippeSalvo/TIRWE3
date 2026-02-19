using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class TreinoService : ITreinoService
{
    private readonly ITreinoRepository _treinoRepository;
    private readonly IMapper _mapper;

    public TreinoService(ITreinoRepository treinoRepository, IMapper mapper)
    {
        _treinoRepository = treinoRepository;
        _mapper = mapper;
    }

    private static Exercicio MapExercicioWithVolume(ExercicioInputDto input)
    {
        var volumeTotal = input.Series * input.Repeticoes * input.Carga;
        return new Exercicio
        {
            Nome = input.Nome,
            GrupoMuscular = input.GrupoMuscular,
            Series = input.Series,
            Repeticoes = input.Repeticoes,
            Carga = input.Carga,
            VolumeTotal = volumeTotal
        };
    }

    public async Task<TreinoDto> CreateAsync(string usuarioId, CreateTreinoDto dto)
    {
        var treino = new Treino
        {
            UsuarioId = usuarioId,
            Nome = dto.Nome.Trim(),
            NivelRecomendado = dto.NivelRecomendado.Trim(),
            ListaExercicios = dto.ListaExercicios.Select(MapExercicioWithVolume).ToList(),
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

    public async Task<TreinoDto?> UpdateAsync(string id, string usuarioId, UpdateTreinoDto dto)
    {
        var treino = await _treinoRepository.GetByIdAsync(id);
        if (treino == null || treino.UsuarioId != usuarioId)
            return null;

        treino.Nome = dto.Nome.Trim();
        treino.NivelRecomendado = dto.NivelRecomendado.Trim();
        treino.ListaExercicios = dto.ListaExercicios.Select(MapExercicioWithVolume).ToList();

        await _treinoRepository.UpdateAsync(treino);
        return _mapper.Map<TreinoDto>(treino);
    }

    public async Task<bool> DeleteAsync(string id, string usuarioId)
    {
        var treino = await _treinoRepository.GetByIdAsync(id);
        if (treino == null || treino.UsuarioId != usuarioId)
            return false;
        return await _treinoRepository.DeleteAsync(id);
    }
}
