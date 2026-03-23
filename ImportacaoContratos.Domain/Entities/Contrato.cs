using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Domain.Entities
{
    public class Contrato
    {
        public int Id { get; set; }
        public string NumeroContrato { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public DateTime Vencimento { get; set; }
        public decimal Valor { get; set; }

        //chave estrangeira para o cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        //chave estrangeira para a importação
        public int ImportacaoId { get; set; }
        public Importacao Importacao { get; set; } = null!;

        public int CalcularDiasAtraso(DateTime dataAtual)
        {
            if (Vencimento >= dataAtual)
                return 0;

            return (int)(dataAtual - Vencimento).TotalDays;
        }
    }
}
