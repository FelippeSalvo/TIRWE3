namespace FitnessTracker.DTOs;

/// <summary>
/// DTO de saída - dados do usuário (sem senha).
/// </summary>
public class UsuarioDto
{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Peso { get; set; }
    public decimal Altura { get; set; }
    public int Idade { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public int MesesDeTreino { get; set; }
    public string Nivel { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
