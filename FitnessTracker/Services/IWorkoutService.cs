using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface IWorkoutService
{
    Task<IEnumerable<WorkoutDto>> GetByUserIdAsync(string userId);
    Task<WorkoutDto?> GetByIdAsync(string id);
    Task<WorkoutDto> CreateAsync(string userId, CreateWorkoutDto createWorkoutDto);
    Task<WorkoutDto?> UpdateAsync(string id, string userId, UpdateWorkoutDto updateWorkoutDto);
    Task<bool> DeleteAsync(string id, string userId);
}
