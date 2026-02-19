using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class TreinoRepository : ITreinoRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Treino> _collection;

    public TreinoRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<Treino>("Treinos");
    }

    public async Task<Treino?> GetByIdAsync(string id)
    {
        return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Treino>> GetByUsuarioIdAsync(string usuarioId)
    {
        return await _collection.Find(t => t.UsuarioId == usuarioId).ToListAsync();
    }

    public async Task<Treino> CreateAsync(Treino treino)
    {
        await _collection.InsertOneAsync(treino);
        return treino;
    }

    public async Task<Treino> UpdateAsync(Treino treino)
    {
        await _collection.ReplaceOneAsync(t => t.Id == treino.Id, treino);
        return treino;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(t => t.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var treino = await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
        return treino != null;
    }
}
