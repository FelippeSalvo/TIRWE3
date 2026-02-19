using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("nome")]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("senhaHash")]
    public string SenhaHash { get; set; } = string.Empty;

    [BsonElement("peso")]
    public decimal Peso { get; set; }

    [BsonElement("altura")]
    public decimal Altura { get; set; }

    [BsonElement("idade")]
    public int Idade { get; set; }

    [BsonElement("sexo")]
    public string Sexo { get; set; } = string.Empty; // M, F, Outro

    [BsonElement("mesesDeTreino")]
    public int MesesDeTreino { get; set; }

    [BsonElement("nivel")]
    public string Nivel { get; set; } = string.Empty; // Iniciante, Intermediário, Avançado

    [BsonElement("dataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
