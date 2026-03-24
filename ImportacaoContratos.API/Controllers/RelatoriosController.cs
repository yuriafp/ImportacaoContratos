using ImportacaoContratos.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImportacaoContratos.API.Controllers;

[Authorize]
public class RelatoriosController : ControllerBase
{
    private readonly ObterRelatoriosUseCase _relatoriosUseCase;

    public RelatoriosController(ObterRelatoriosUseCase relatoriosUseCase)
    {
        _relatoriosUseCase = relatoriosUseCase;
    }

    [HttpGet("importacoes")]
    public async Task<IActionResult> GetImportacoes() =>
        Ok(await _relatoriosUseCase.ExecutarHistoricoImportacoesAsync());

    [HttpGet("clientes-resumo")]
    public async Task<IActionResult> GetResumoClientes() =>
        Ok(await _relatoriosUseCase.ExecutarResumoClientesAsync());

    [HttpGet("contratos")]
    public async Task<IActionResult> GetTodosContratos()
    {
        var contratos = await _relatoriosUseCase.ExecutarListagemContratosAsync();

        return Ok(contratos);
    }
}