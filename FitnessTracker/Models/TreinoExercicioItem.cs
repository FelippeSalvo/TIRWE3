using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

/// <summary>
/// Referência a um exercício do catálogo com número de séries para este treino.
/// </summary>
public class TreinoExercicioItem
{
    [BsonElement("exerciseId")]
    public string ExerciseId { get; set; } = string.Empty;

    [BsonElement("series")]
    public int Series { get; set; } = 3; // padrão para cálculo de volume
}
