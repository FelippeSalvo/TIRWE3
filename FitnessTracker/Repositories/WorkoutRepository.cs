using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Workout> _collection;

    public WorkoutRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<Workout>("Workouts");
    }

    public async Task<Workout?> GetByIdAsync(string id)
    {
        return await _collection.Find(w => w.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAsync(string userId)
    {
        return await _collection.Find(w => w.UserId == userId).ToListAsync();
    }

    public async Task<Workout> CreateAsync(Workout workout)
    {
        await _collection.InsertOneAsync(workout);
        return workout;
    }

    public async Task<Workout> UpdateAsync(Workout workout)
    {
        workout.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(w => w.Id == workout.Id, workout);
        return workout;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(w => w.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var workout = await _collection.Find(w => w.Id == id).FirstOrDefaultAsync();
        return workout != null;
    }
}
