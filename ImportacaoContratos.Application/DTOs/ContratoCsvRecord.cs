using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Application.DTOs
{
    public class ContratoCsvRecord
    {
        [Name("Nome")]
        public string Nome { get; set; } = string.Empty;
        [Name("Cpf")]
        public string Cpf { get; set; } = string.Empty;
        [Name("Contrato")]
        public string Contrato { get; set; } = string.Empty;
        [Name("Produto")]
        public string Produto { get; set; } = string.Empty;
        [Name("Vencimento")]
        public DateTime Vencimento { get; set; }
        [Name("Valor")]
        public decimal Valor { get; set; }
    }
}
