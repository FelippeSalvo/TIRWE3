namespace FitnessTracker.DTOs;

public class ExerciseDto
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string GrupoMuscularPrincipal { get; set; } = string.Empty;
    public List<string> GruposMuscularesSecundarios { get; set; } = new();
    public string TipoMovimento { get; set; } = string.Empty;
    public string PlanoMovimento { get; set; } = string.Empty;
    public bool Multiarticulado { get; set; }
    public string NivelDificuldade { get; set; } = string.Empty;
    public string Equipamento { get; set; } = string.Empty;
    public string PadraoMovimento { get; set; } = string.Empty;
    public int FatorFadiga { get; set; }
    public string SimilaridadeGrupo { get; set; } = string.Empty;
}
