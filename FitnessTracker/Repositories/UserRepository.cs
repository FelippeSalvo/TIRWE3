using FitnessTracker.Data;
using FitnessTracker.Models;
using MongoDB.Driver;

namespace FitnessTracker.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoContext _context;
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IMongoContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<User>("Users");
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _collection.InsertOneAsync(user);
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);
        return user;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(u => u.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var user = await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        return user != null;
    }
}
