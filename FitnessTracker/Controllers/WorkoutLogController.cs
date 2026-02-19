using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutLogController : ControllerBase
{
    private readonly IWorkoutLogService _workoutLogService;
    private readonly ILogger<WorkoutLogController> _logger;

    public WorkoutLogController(IWorkoutLogService workoutLogService, ILogger<WorkoutLogController> logger)
    {
        _workoutLogService = workoutLogService;
        _logger = logger;
    }

    private string? GetUsuarioId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

    /// <summary>
    /// Registra uma execução de exercício (registro de treino).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WorkoutLogDto>> Registrar([FromBody] RegistrarWorkoutLogDto dto)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var log = await _workoutLogService.RegistrarAsync(usuarioId, dto);
        return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
    }

    /// <summary>
    /// Obtém um registro de treino por ID (apenas do usuário autenticado).
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutLogDto>> GetById(string id)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId)) return Unauthorized();
        var log = await _workoutLogService.GetByIdAsync(id, usuarioId);
        if (log == null) return NotFound();
        return Ok(log);
    }

    /// <summary>
    /// Retorna o histórico de registros de treino do usuário (ordenado por data decrescente).
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutLogDto>>> GetHistorico()
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var historico = await _workoutLogService.GetHistoricoPorUsuarioAsync(usuarioId);
        return Ok(historico);
    }
}
