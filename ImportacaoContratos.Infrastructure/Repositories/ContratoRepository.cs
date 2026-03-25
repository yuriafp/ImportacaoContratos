using ImportacaoContratos.Domain.Entities;
using ImportacaoContratos.Domain.Interfaces;
using ImportacaoContratos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImportacaoContratos.Infrastructure.Repositories;

public class ContratoRepository : IContratoRepository
{
    private readonly AppDbContext _context;

    public ContratoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> ObterClientePorCpfAsync(string cpf)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf);
    }

    public async Task SalvarImportacaoAsync(Importacao importacao)
    {
        await _context.Importacoes.AddAsync(importacao);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Importacao>> ObterHistoricoImportacoesAsync()
    {
        return await _context.Importacoes
            .Include(i => i.Usuario)
            .Include(i => i.Contratos)
            .OrderByDescending(i => i.DataImportacao)
            .ToListAsync();
    }

    public async Task<List<Cliente>> ObterResumoClientesAsync()
    {
        return await _context.Clientes
            .Include(c => c.Contratos)
            .ToListAsync();
    }

    public async Task<List<Contrato>> ObterTodosContratosAsync()
    {
        return await _context.Contratos
        .Include(c => c.Cliente)
        .OrderBy(c => c.Vencimento)
        .ToListAsync();
    }

    public async Task<Contrato> ObterContratoPorNumeroAsync(string numeroContrato)
    {
        return await _context.Contratos.FirstOrDefaultAsync(c => c.NumeroContrato == numeroContrato);
    }
}