using ImportacaoContratos.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ImportacaoContratos.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ContratosController : ControllerBase
{
    private readonly ImportarContratosUseCase _useCase;

    public ContratosController(ImportarContratosUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost("importar")]
    public async Task<IActionResult> ImportarArquivo(IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
            return BadRequest("Nenhum arquivo foi enviado.");

        if (!arquivo.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            return BadRequest("O arquivo tem de ser um .csv.");

        var usuarioIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int usuarioId = int.Parse(usuarioIdString!);

        using var stream = arquivo.OpenReadStream();

        var errosDeImportacao = await _useCase.ExecutarAsync(stream, arquivo.FileName, usuarioId);

        //valida erros e retorna as linhas que falharam, mas importa as linhas válidas
        if (errosDeImportacao.Any())
        {
            return Ok(new
            {
                mensagem = "Ficheiro importado, mas algumas linhas continham erros e foram ignoradas.",
                erros = errosDeImportacao
            });
        }

        return Ok(new { mensagem = "Arquivo importado com sucesso para a base de dados!" });
    }
}