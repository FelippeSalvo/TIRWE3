using FitnessTracker.Models;

namespace FitnessTracker.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(string id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsByEmailAsync(string email);
}
