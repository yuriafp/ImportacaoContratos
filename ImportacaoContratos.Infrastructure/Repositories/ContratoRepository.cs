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
}