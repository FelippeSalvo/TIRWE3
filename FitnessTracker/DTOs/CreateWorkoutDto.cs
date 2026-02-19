using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

public class CreateWorkoutDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
    public int Duration { get; set; }

    [Range(0, int.MaxValue)]
    public int? CaloriesBurned { get; set; }

    public DateTime? WorkoutDate { get; set; }
}
