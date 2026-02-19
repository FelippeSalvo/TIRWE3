using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

public class WorkoutLogDto
{
    public string Id { get; set; } = string.Empty;
    public string UsuarioId { get; set; } = string.Empty;
    public string ExercicioNome { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public decimal Carga { get; set; }
    public int Repeticoes { get; set; }
    public decimal Volume { get; set; }
}

public class RegistrarWorkoutLogDto
{
    [Required(ErrorMessage = "Nome do exercício é obrigatório")]
    [StringLength(100)]
    public string ExercicioNome { get; set; } = string.Empty;

    public DateTime? Data { get; set; } // opcional; se null, usa Data atual

    [Required]
    [Range(0, 1000, ErrorMessage = "Carga deve ser entre 0 e 1000 kg")]
    public decimal Carga { get; set; }

    [Required]
    [Range(1, 500, ErrorMessage = "Repetições deve ser entre 1 e 500")]
    public int Repeticoes { get; set; }
}
