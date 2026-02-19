using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

public class ExercicioDto
{
    public string Nome { get; set; } = string.Empty;
    public string GrupoMuscular { get; set; } = string.Empty;
    public int Series { get; set; }
    public int Repeticoes { get; set; }
    public decimal Carga { get; set; }
    public decimal VolumeTotal { get; set; }
}

public class ExercicioInputDto
{
    [Required(ErrorMessage = "Nome do exercício é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grupo muscular é obrigatório")]
    [StringLength(80)]
    public string GrupoMuscular { get; set; } = string.Empty;

    [Required]
    [Range(1, 20, ErrorMessage = "Séries deve ser entre 1 e 20")]
    public int Series { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Repetições deve ser entre 1 e 100")]
    public int Repeticoes { get; set; }

    [Required]
    [Range(0, 1000, ErrorMessage = "Carga deve ser entre 0 e 1000 kg")]
    public decimal Carga { get; set; }
}
