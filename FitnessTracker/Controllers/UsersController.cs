using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUsuarioService usuarioService, ILogger<UsersController> logger)
    {
        _usuarioService = usuarioService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém os dados do usuário autenticado.
    /// </summary>
    [HttpGet("me")]
    public async Task<ActionResult<UsuarioDto>> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var usuario = await _usuarioService.GetByIdAsync(userId);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }
}
