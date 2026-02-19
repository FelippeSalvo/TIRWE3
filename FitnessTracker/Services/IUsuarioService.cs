using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface IUsuarioService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task<UsuarioDto?> GetByIdAsync(string id);
}
