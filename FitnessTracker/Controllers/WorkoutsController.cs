using FitnessTracker.DTOs;
using FitnessTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;
    private readonly ILogger<WorkoutsController> _logger;

    public WorkoutsController(IWorkoutService workoutService, ILogger<WorkoutsController> logger)
    {
        _workoutService = workoutService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var workouts = await _workoutService.GetByUserIdAsync(userId);
        return Ok(workouts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutDto>> GetById(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var workout = await _workoutService.GetByIdAsync(id);

        if (workout == null)
        {
            return NotFound();
        }

        if (workout.UserId != userId)
        {
            return Forbid();
        }

        return Ok(workout);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutDto>> Create([FromBody] CreateWorkoutDto createWorkoutDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var workout = await _workoutService.CreateAsync(userId, createWorkoutDto);
        return CreatedAtAction(nameof(GetById), new { id = workout.Id }, workout);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkoutDto>> Update(string id, [FromBody] UpdateWorkoutDto updateWorkoutDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var workout = await _workoutService.UpdateAsync(id, userId, updateWorkoutDto);

        if (workout == null)
        {
            return NotFound();
        }

        return Ok(workout);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var deleted = await _workoutService.DeleteAsync(id, userId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
