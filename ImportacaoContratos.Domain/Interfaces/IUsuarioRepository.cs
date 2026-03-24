using ImportacaoContratos.Domain.Entities;

namespace ImportacaoContratos.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
}