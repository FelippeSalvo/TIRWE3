using AutoMapper;
using FitnessTracker.DTOs;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Repositories;

namespace FitnessTracker.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly JwtHelper _jwtHelper;

    public UsuarioService(
        IUsuarioRepository usuarioRepository,
        IMapper mapper,
        JwtHelper jwtHelper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _jwtHelper = jwtHelper;
    }

    /// <summary>
    /// Calcula o nível do usuário baseado nos meses de treino.
    /// < 6 meses = Iniciante | 6-24 meses = Intermediário | > 24 meses = Avançado
    /// </summary>
    public static string CalcularNivel(int mesesDeTreino)
    {
        return mesesDeTreino switch
        {
            < 6 => "Iniciante",
            <= 24 => "Intermediário",
            _ => "Avançado"
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await _usuarioRepository.ExistsByEmailAsync(registerDto.Email))
        {
            throw new InvalidOperationException("Email já está em uso");
        }

        var nivel = CalcularNivel(registerDto.MesesDeTreino);

        var usuario = new Usuario
        {
            Nome = registerDto.Nome,
            Email = registerDto.Email.Trim().ToLower(),
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Senha),
            Peso = registerDto.Peso,
            Altura = registerDto.Altura,
            Idade = registerDto.Idade,
            Sexo = registerDto.Sexo,
            MesesDeTreino = registerDto.MesesDeTreino,
            Nivel = nivel,
            DataCriacao = DateTime.UtcNow
        };

        await _usuarioRepository.CreateAsync(usuario);

        var token = _jwtHelper.GenerateToken(usuario.Id, usuario.Email);
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

        return new AuthResponseDto
        {
            Token = token,
            Usuario = usuarioDto
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDto.Senha, usuario.SenhaHash))
        {
            return null;
        }

        var token = _jwtHelper.GenerateToken(usuario.Id, usuario.Email);
        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

        return new AuthResponseDto
        {
            Token = token,
            Usuario = usuarioDto
        };
    }

    public async Task<UsuarioDto?> GetByIdAsync(string id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return usuario == null ? null : _mapper.Map<UsuarioDto>(usuario);
    }
}
