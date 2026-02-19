using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

public class Workout
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("userId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("duration")]
    public int Duration { get; set; } // em minutos

    [BsonElement("caloriesBurned")]
    public int? CaloriesBurned { get; set; }

    [BsonElement("workoutDate")]
    public DateTime WorkoutDate { get; set; } = DateTime.UtcNow;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
