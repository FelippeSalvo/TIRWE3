using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessTracker.Models;

/// <summary>
/// Exercício pré-cadastrado do banco. Não é criado pelo usuário; apenas referenciado nos treinos.
/// </summary>
public class Exercise
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("nome")]
    public string Nome { get; set; } = string.Empty;

    [BsonElement("grupoMuscularPrincipal")]
    public string GrupoMuscularPrincipal { get; set; } = string.Empty;

    [BsonElement("gruposMuscularesSecundarios")]
    public List<string> GruposMuscularesSecundarios { get; set; } = new();

    [BsonElement("tipoMovimento")]
    public string TipoMovimento { get; set; } = string.Empty; // Empurrar, Puxar, Agachar, Levantar, Isolado

    [BsonElement("planoMovimento")]
    public string PlanoMovimento { get; set; } = string.Empty; // Sagital, Frontal, Transversal

    [BsonElement("multiarticulado")]
    public bool Multiarticulado { get; set; }

    [BsonElement("nivelDificuldade")]
    public string NivelDificuldade { get; set; } = string.Empty; // Iniciante, Intermediário, Avançado

    [BsonElement("equipamento")]
    public string Equipamento { get; set; } = string.Empty; // Barra, Halter, Máquina, Peso corporal

    [BsonElement("padraoMovimento")]
    public string PadraoMovimento { get; set; } = string.Empty;

    [BsonElement("fatorFadiga")]
    public int FatorFadiga { get; set; } = 1; // 1 a 5

    [BsonElement("similaridadeGrupo")]
    public string SimilaridadeGrupo { get; set; } = string.Empty;
}
