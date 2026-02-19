namespace FitnessTracker.DTOs;

public class WorkoutDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int? CaloriesBurned { get; set; }
    public DateTime WorkoutDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
