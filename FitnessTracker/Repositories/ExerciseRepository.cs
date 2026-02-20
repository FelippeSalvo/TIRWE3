using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Exercise> _collection;

    public ExerciseRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<Exercise>("Exercises");
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync()
    {
        return await _collection.Find(FilterDefinition<Exercise>.Empty).ToListAsync();
    }

    public async Task<Exercise?> GetByIdAsync(string id)
    {
        return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Exercise>> GetByIdsAsync(IEnumerable<string> ids)
    {
        var idList = ids.ToList();
        if (idList.Count == 0)
            return Array.Empty<Exercise>();
        var filter = Builders<Exercise>.Filter.In(e => e.Id, idList);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<bool> AnyAsync()
    {
        return await _collection.Find(FilterDefinition<Exercise>.Empty).Limit(1).AnyAsync();
    }

    public async Task InsertManyAsync(IEnumerable<Exercise> exercises)
    {
        var list = exercises.ToList();
        if (list.Count == 0) return;
        await _collection.InsertManyAsync(list);
    }
}
