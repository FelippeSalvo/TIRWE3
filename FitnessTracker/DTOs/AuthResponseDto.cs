namespace FitnessTracker.DTOs;

/// <summary>
/// DTO de saída - resposta do login/registro com token JWT.
/// </summary>
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public UsuarioDto Usuario { get; set; } = null!;
}
