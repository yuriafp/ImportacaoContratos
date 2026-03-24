using ImportacaoContratos.Application.DTOs;
using ImportacaoContratos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImportacaoContratos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegistroUsuarioDto dto)
    {
        await _authService.RegistrarAsync(dto);
        return Ok(new { mensagem = "Usuário registrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);

        return Ok(new { Token = token });
    }
}