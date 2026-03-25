using System.ComponentModel.DataAnnotations;

namespace ImportacaoContratos.BlazorUI.Models;

public class LoginDto
{
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    public string Senha { get; set; } = string.Empty;
}

public class LoginResult
{
    public string Token { get; set; } = string.Empty;
}