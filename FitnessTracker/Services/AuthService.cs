using AutoMapper;
using FitnessTracker.Config;
using FitnessTracker.DTOs;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IUserRepository userRepository, IMapper mapper, JwtSettings jwtSettings)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtHelper = new JwtHelper(jwtSettings);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userRepository.ExistsByEmailAsync(registerDto.Email))
        {
            throw new InvalidOperationException("Email already registered");
        }

        var user = new User
        {
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateAsync(user);

        var token = _jwtHelper.GenerateToken(user.Id, user.Email);
        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        var token = _jwtHelper.GenerateToken(user.Id, user.Email);
        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }
}
