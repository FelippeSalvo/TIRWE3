using FitnessTracker.Models;

namespace FitnessTracker.Repositories;

public interface IExerciseRepository
{
    Task<IEnumerable<Exercise>> GetAllAsync();
    Task<Exercise?> GetByIdAsync(string id);
    Task<IEnumerable<Exercise>> GetByIdsAsync(IEnumerable<string> ids);
    Task<bool> AnyAsync();
    Task InsertManyAsync(IEnumerable<Exercise> exercises);
}
