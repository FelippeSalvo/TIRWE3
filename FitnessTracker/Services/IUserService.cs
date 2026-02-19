using FitnessTracker.DTOs;

namespace FitnessTracker.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(string id);
    Task<UserDto?> GetByEmailAsync(string email);
}
