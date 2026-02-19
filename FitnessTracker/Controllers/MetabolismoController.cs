using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MetabolismoController : ControllerBase
{
    private readonly IMetabolismoService _metabolismoService;
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<MetabolismoController> _logger;

    public MetabolismoController(
        IMetabolismoService metabolismoService,
        IUsuarioService usuarioService,
        ILogger<MetabolismoController> logger)
    {
        _metabolismoService = metabolismoService;
        _usuarioService = usuarioService;
        _logger = logger;
    }

    /// <summary>
    /// Retorna a TMB (Taxa Metabólica Basal) do usuário autenticado, calculada pela fórmula Mifflin-St Jeor.
    /// </summary>
    [HttpGet("tmb")]
    public async Task<ActionResult<object>> GetTmb()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var usuario = await _usuarioService.GetByIdAsync(userId);
        if (usuario == null)
            return NotFound(new { message = "Usuário não encontrado" });

        var tmb = _metabolismoService.CalcularTMB(usuario.Peso, usuario.Altura, usuario.Idade, usuario.Sexo);

        return Ok(new
        {
            tmb = Math.Round(tmb, 2),
            unidade = "kcal/dia",
            formula = "Mifflin-St Jeor"
        });
    }
}
