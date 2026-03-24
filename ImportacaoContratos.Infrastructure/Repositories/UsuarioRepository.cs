using ImportacaoContratos.Domain.Entities;
using ImportacaoContratos.Domain.Interfaces;
using ImportacaoContratos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImportacaoContratos.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
}