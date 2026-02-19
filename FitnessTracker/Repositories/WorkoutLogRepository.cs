using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class WorkoutLogRepository : IWorkoutLogRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<WorkoutLog> _collection;

    public WorkoutLogRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<WorkoutLog>("WorkoutLogs");
    }

    public async Task<WorkoutLog?> GetByIdAsync(string id)
    {
        return await _collection.Find(l => l.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<WorkoutLog>> GetByUsuarioIdAsync(string usuarioId)
    {
        return await _collection
            .Find(l => l.UsuarioId == usuarioId)
            .SortByDescending(l => l.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkoutLog>> GetByUsuarioIdAsync(string usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        var filter = Builders<WorkoutLog>.Filter.And(
            Builders<WorkoutLog>.Filter.Eq(l => l.UsuarioId, usuarioId),
            Builders<WorkoutLog>.Filter.Gte(l => l.Data, dataInicio),
            Builders<WorkoutLog>.Filter.Lte(l => l.Data, dataFim)
        );
        return await _collection.Find(filter).SortByDescending(l => l.Data).ToListAsync();
    }

    public async Task<WorkoutLog> CreateAsync(WorkoutLog log)
    {
        await _collection.InsertOneAsync(log);
        return log;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(l => l.Id == id);
        return result.DeletedCount > 0;
    }
}
