using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;

    public ExercisesController(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista todos os exercícios do catálogo pré-cadastrado. O usuário não pode criar exercícios; apenas referenciar por ID nos treinos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
    {
        var exercises = await _exerciseRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ExerciseDto>>(exercises));
    }
}
