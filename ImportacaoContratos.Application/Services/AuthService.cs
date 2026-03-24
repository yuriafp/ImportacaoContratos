using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImportacaoContratos.Application.DTOs;
using ImportacaoContratos.Domain.Entities;
using ImportacaoContratos.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace ImportacaoContratos.Application.Services;

public class AuthService
{
    private readonly IUsuarioRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task RegistrarAsync(RegistroUsuarioDto dto)
    {
        var usuarioExistente = await _repository.ObterPorEmailAsync(dto.Email);
        if (usuarioExistente != null)
            throw new ArgumentException("Este email já está registado.");

        string senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        var novoUsuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = senhaHash
        };

        await _repository.AdicionarAsync(novoUsuario);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var usuario = await _repository.ObterPorEmailAsync(dto.Email);
        if (usuario == null)
            throw new UnauthorizedAccessException("Email ou senha inválidos.");

        bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash);
        if (!senhaValida)
            throw new UnauthorizedAccessException("Email ou senha inválidos.");

        return GerarTokenJwt(usuario);
    }

    private string GerarTokenJwt(Usuario usuario)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? throw new Exception("Chave JWT não configurada.");
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}