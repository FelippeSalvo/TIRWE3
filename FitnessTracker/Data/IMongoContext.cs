using MongoDB.Driver;

namespace FitnessTracker.Data;

public interface IMongoContext
{
    IMongoDatabase Database { get; }
}
