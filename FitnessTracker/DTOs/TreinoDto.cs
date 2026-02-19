using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

public class TreinoDto
{
    public string Id { get; set; } = string.Empty;
    public string UsuarioId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string NivelRecomendado { get; set; } = string.Empty;
    public List<ExercicioDto> ListaExercicios { get; set; } = new();
    public DateTime DataCriacao { get; set; }
}

public class CreateTreinoDto
{
    [Required(ErrorMessage = "Nome do treino é obrigatório")]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nível recomendado é obrigatório")]
    [RegularExpression("^(Iniciante|Intermediário|Avançado)$", ErrorMessage = "Nível deve ser: Iniciante, Intermediário ou Avançado")]
    public string NivelRecomendado { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Informe ao menos um exercício")]
    public List<ExercicioInputDto> ListaExercicios { get; set; } = new();
}

public class UpdateTreinoDto
{
    [Required(ErrorMessage = "Nome do treino é obrigatório")]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nível recomendado é obrigatório")]
    [RegularExpression("^(Iniciante|Intermediário|Avançado)$", ErrorMessage = "Nível deve ser: Iniciante, Intermediário ou Avançado")]
    public string NivelRecomendado { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Informe ao menos um exercício")]
    public List<ExercicioInputDto> ListaExercicios { get; set; } = new();
}
