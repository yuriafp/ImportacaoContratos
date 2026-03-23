using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Application.DTOs
{
    public class ContratoCsvRecord
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Contrato { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }
    }
}
