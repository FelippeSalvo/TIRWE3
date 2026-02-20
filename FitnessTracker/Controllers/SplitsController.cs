using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SplitsController : ControllerBase
{
    private readonly ISplitTemplateService _splitTemplateService;

    public SplitsController(ISplitTemplateService splitTemplateService)
    {
        _splitTemplateService = splitTemplateService;
    }

    /// <summary>
    /// Retorna os templates de divisão de treino: PushPullLegs, UpperLower, BroSplit e Personalizado, com dias, grupos por dia e frequência semanal sugerida.
    /// </summary>
    [HttpGet("templates")]
    public async Task<ActionResult<IEnumerable<SplitTemplateDto>>> GetTemplates()
    {
        var templates = await _splitTemplateService.GetTemplatesAsync();
        return Ok(templates);
    }
}
