using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

public class TreinoDto
{
    public string Id { get; set; } = string.Empty;
    public string UsuarioId { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string GrupoMuscularFoco { get; set; } = string.Empty;
    public List<TreinoExercicioItemDto> ListaExercicios { get; set; } = new();
    public DateTime DataCriacao { get; set; }
    public string DivisaoTreino { get; set; } = "Personalizado";
}

public class TreinoExercicioItemDto
{
    public string ExerciseId { get; set; } = string.Empty;
    public int Series { get; set; } = 3;
}

public class CreateTreinoDto
{
    [Required(ErrorMessage = "Nome do treino é obrigatório")]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grupo muscular foco é obrigatório")]
    [StringLength(80)]
    public string GrupoMuscularFoco { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Informe ao menos um exercício")]
    public List<TreinoExercicioItemInputDto> ListaExercicios { get; set; } = new();

    [Required]
    [RegularExpression("^(PushPullLegs|UpperLower|BroSplit|Personalizado)$", ErrorMessage = "Divisão deve ser: PushPullLegs, UpperLower, BroSplit ou Personalizado")]
    public string DivisaoTreino { get; set; } = "Personalizado";
}

public class TreinoExercicioItemInputDto
{
    [Required]
    public string ExerciseId { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "Séries entre 1 e 10")]
    public int Series { get; set; } = 3;
}

public class UpdateTreinoDto
{
    [Required(ErrorMessage = "Nome do treino é obrigatório")]
    [StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grupo muscular foco é obrigatório")]
    [StringLength(80)]
    public string GrupoMuscularFoco { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Informe ao menos um exercício")]
    public List<TreinoExercicioItemInputDto> ListaExercicios { get; set; } = new();

    [Required]
    [RegularExpression("^(PushPullLegs|UpperLower|BroSplit|Personalizado)$")]
    public string DivisaoTreino { get; set; } = "Personalizado";
}
