using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TreinoController : ControllerBase
{
    private readonly ITreinoService _treinoService;
    private readonly ITreinoAnalysisService _treinoAnalysisService;
    private readonly IAdvancedTrainingAnalysisService _advancedAnalysisService;
    private readonly ILogger<TreinoController> _logger;

    public TreinoController(ITreinoService treinoService, ITreinoAnalysisService treinoAnalysisService, IAdvancedTrainingAnalysisService advancedAnalysisService, ILogger<TreinoController> logger)
    {
        _treinoService = treinoService;
        _treinoAnalysisService = treinoAnalysisService;
        _advancedAnalysisService = advancedAnalysisService;
        _logger = logger;
    }

    private string? GetUsuarioId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

    /// <summary>
    /// Lista todos os treinos do usuário autenticado.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoDto>>> GetAll()
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var treinos = await _treinoService.GetByUsuarioIdAsync(usuarioId);
        return Ok(treinos);
    }

    /// <summary>
    /// Obtém um treino por ID (apenas se pertencer ao usuário).
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<TreinoDto>> GetById(string id)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var treino = await _treinoService.GetByIdAsync(id, usuarioId);
        if (treino == null)
            return NotFound(new { message = "Treino não encontrado" });

        return Ok(treino);
    }

    /// <summary>
    /// Cria um novo treino para o usuário autenticado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TreinoDto>> Create([FromBody] CreateTreinoDto dto)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var treino = await _treinoService.CreateAsync(usuarioId, dto);
        if (treino == null)
            return BadRequest(new { message = "Um ou mais ExerciseIds não existem no catálogo. Use apenas IDs retornados por GET /api/exercises." });
        return CreatedAtAction(nameof(GetById), new { id = treino.Id }, treino);
    }

    /// <summary>
    /// Atualiza um treino existente (apenas se pertencer ao usuário).
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<TreinoDto>> Update(string id, [FromBody] UpdateTreinoDto dto)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var result = await _treinoService.UpdateAsync(id, usuarioId, dto);
        if (result.Error == "NotFound")
            return NotFound(new { message = "Treino não encontrado" });
        if (result.Error == "InvalidExerciseIds")
            return BadRequest(new { message = "Um ou mais ExerciseIds não existem no catálogo." });
        return Ok(result.Treino);
    }

    /// <summary>
    /// Remove um treino (apenas se pertencer ao usuário).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var deleted = await _treinoService.DeleteAsync(id, usuarioId);
        if (!deleted)
            return NotFound(new { message = "Treino não encontrado" });

        return NoContent();
    }

    /// <summary>
    /// Análise de qualidade do treino: score 0-100 e sugestões (volume, frequência por grupo, distribuição, séries).
    /// </summary>
    [HttpGet("analise/{idTreino}")]
    public async Task<ActionResult<TreinoAnaliseDto>> GetAnalise(string idTreino)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var analise = await _treinoAnalysisService.AnalisarAsync(idTreino, usuarioId);
        if (analise == null)
            return NotFound(new { message = "Treino não encontrado" });

        return Ok(analise);
    }

    /// <summary>
    /// Análise avançada: volume por músculo, redundância, fadiga, equilíbrio, adequação ao nível, frequência. Score 0-100, classificação, pontos fortes/fracos e sugestões.
    /// </summary>
    [HttpGet("advanced-analysis/{idTreino}")]
    public async Task<ActionResult<AdvancedAnalysisDto>> GetAdvancedAnalysis(string idTreino)
    {
        var usuarioId = GetUsuarioId();
        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        var analise = await _advancedAnalysisService.AnalyzeAsync(idTreino, usuarioId);
        if (analise == null)
            return NotFound(new { message = "Treino não encontrado" });

        return Ok(analise);
    }
}
