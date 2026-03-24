using ImportacaoContratos.Application.DTOs;
using ImportacaoContratos.Domain.Interfaces;

namespace ImportacaoContratos.Application.UseCases;

public class ObterRelatoriosUseCase
{
    private readonly IContratoRepository _repository;

    public ObterRelatoriosUseCase(IContratoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ImportacaoResumoDto>> ExecutarHistoricoImportacoesAsync()
    {
        var importacoes = await _repository.ObterHistoricoImportacoesAsync();

        return importacoes.Select(i => new ImportacaoResumoDto
        {
            Id = i.Id,
            NomeArquivo = i.NomeArquivo,
            DataImportacao = i.DataImportacao,
            NomeUsuario = i.Usuario.Nome,
            TotalContratos = i.Contratos.Count
        }).ToList();
    }

    public async Task<List<ClienteResumoDto>> ExecutarResumoClientesAsync()
    {
        var clientes = await _repository.ObterResumoClientesAsync();
        var dataAtual = DateTime.Now.Date;

        return clientes.Select(c =>
        {
            var contratosDto = c.Contratos.Select(ct => new ContratoResumoItemDto
            {
                NumeroContrato = ct.NumeroContrato,
                Produto = ct.Produto,
                Valor = ct.Valor,
                Vencimento = ct.Vencimento,
                DiasAtraso = ct.Vencimento < dataAtual ? (dataAtual - ct.Vencimento.Date).Days : 0
            }).ToList();

            return new ClienteResumoDto
            {
                NomeCliente = c.Nome,
                Cpf = c.Cpf,
                ValorTotalContratos = contratosDto.Sum(x => x.Valor),
                MaiorAtrasoDias = contratosDto.Any() ? contratosDto.Max(x => x.DiasAtraso) : 0,
                Contratos = contratosDto
            };
        })
        .OrderByDescending(x => x.MaiorAtrasoDias)
        .ToList();
    }

    public async Task<List<ContratoDto>> ExecutarListagemContratosAsync()
    {
        var contratos = await _repository.ObterTodosContratosAsync();

        return contratos.Select(c => new ContratoDto
        {
            NumeroContrato = c.NumeroContrato,
            Produto = c.Produto,
            Valor = c.Valor,
            Vencimento = c.Vencimento,
            NomeCliente = c.Cliente.Nome,
            CpfCliente = c.Cliente.Cpf
        }).ToList();
    }
}