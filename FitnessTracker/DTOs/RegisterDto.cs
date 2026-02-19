using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs;

/// <summary>
/// DTO de entrada para registro de usuário.
/// </summary>
public class RegisterDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public string Senha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Peso é obrigatório")]
    [Range(1, 500, ErrorMessage = "Peso deve estar entre 1 e 500 kg")]
    public decimal Peso { get; set; }

    [Required(ErrorMessage = "Altura é obrigatória")]
    [Range(0.5, 3, ErrorMessage = "Altura deve estar entre 0.5 e 3 metros")]
    public decimal Altura { get; set; }

    [Required(ErrorMessage = "Idade é obrigatória")]
    [Range(10, 120, ErrorMessage = "Idade deve estar entre 10 e 120 anos")]
    public int Idade { get; set; }

    [Required(ErrorMessage = "Sexo é obrigatório")]
    [RegularExpression("^(M|F|Outro)$", ErrorMessage = "Sexo deve ser M, F ou Outro")]
    public string Sexo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Meses de treino é obrigatório")]
    [Range(0, 600, ErrorMessage = "Meses de treino deve ser entre 0 e 600")]
    public int MesesDeTreino { get; set; }
}
