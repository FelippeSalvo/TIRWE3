using FitnessTracker.Config;
using MongoDB.Driver;

namespace FitnessTracker.Data;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoDatabase Database => _database;
}
