using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

public class Exercicio
{
    [BsonElement("nome")]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("grupoMuscular")]
    public string GrupoMuscular { get; set; } = string.Empty;

    [BsonElement("series")]
    public int Series { get; set; }

    [BsonElement("repeticoes")]
    public int Repeticoes { get; set; }

    [BsonElement("carga")]
    public decimal Carga { get; set; }

    /// <summary>
    /// Volume total = Series × Repeticoes × Carga (calculado automaticamente).
    /// </summary>
    [BsonElement("volumeTotal")]
    public decimal VolumeTotal { get; set; }
}
