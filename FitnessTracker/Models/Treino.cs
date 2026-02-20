using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

public class Treino
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("usuarioId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UsuarioId { get; set; } = string.Empty;

    [BsonElement("nome")]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("grupoMuscularFoco")]
    public string GrupoMuscularFoco { get; set; } = string.Empty;

    [BsonElement("listaExercicios")]
    public List<TreinoExercicioItem> ListaExercicios { get; set; } = new();

    [BsonElement("dataCriacao")]
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    [BsonElement("divisaoTreino")]
    public string DivisaoTreino { get; set; } = "Personalizado"; // PushPullLegs, UpperLower, BroSplit, Personalizado
}
