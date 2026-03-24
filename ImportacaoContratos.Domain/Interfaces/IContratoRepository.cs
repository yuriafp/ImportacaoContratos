using ImportacaoContratos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Domain.Interfaces
{
    public interface IContratoRepository
    {
        Task<Cliente?> ObterClientePorCpfAsync(string cpf);
        Task SalvarImportacaoAsync(Importacao importacao);
    }
}
