using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<Usuario> _collection;

    public UsuarioRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<Usuario>("Usuarios");
    }

    public async Task<Usuario?> GetByIdAsync(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _collection.Find(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await _collection.InsertOneAsync(usuario);
        return usuario;
    }

    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        await _collection.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);
        return usuario;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var usuario = await _collection.Find(u => u.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        return usuario != null;
    }
}
