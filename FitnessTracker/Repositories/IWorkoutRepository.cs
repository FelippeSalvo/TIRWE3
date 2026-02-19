using FitnessTracker.Models;

namespace FitnessTracker.Repositories;

public interface IWorkoutRepository
{
    Task<Workout?> GetByIdAsync(string id);
    Task<IEnumerable<Workout>> GetByUserIdAsync(string userId);
    Task<Workout> CreateAsync(Workout workout);
    Task<Workout> UpdateAsync(Workout workout);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
