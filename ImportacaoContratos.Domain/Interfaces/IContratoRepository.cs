using ImportacaoContratos.Domain.Entities;

namespace ImportacaoContratos.Domain.Interfaces;

public interface IContratoRepository
{
    Task<Cliente?> ObterClientePorCpfAsync(string cpf);
    Task SalvarImportacaoAsync(Importacao importacao);
    Task<List<Importacao>> ObterHistoricoImportacoesAsync();
    Task<List<Contrato>> ObterTodosContratosAsync();
    Task<List<Cliente>> ObterResumoClientesAsync();
    Task<Contrato> ObterContratoPorNumeroAsync(string numeroContrato);
}
