using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

public class WorkoutLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; } = string.Empty;

    [BsonElement("exercicioNome")]
    public string ExercicioNome { get; set; } = string.Empty;

    [BsonElement("data")]
    public DateTime Data { get; set; } = DateTime.UtcNow;

    [BsonElement("carga")]
    public decimal Carga { get; set; }

    [BsonElement("repeticoes")]
    public int Repeticoes { get; set; }

    [BsonElement("volume")]
    public decimal Volume { get; set; } // Carga × Repeticoes (ou por série: pode ser total)
}
