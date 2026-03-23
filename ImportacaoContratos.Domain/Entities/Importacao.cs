using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Domain.Entities
{
    public class Importacao
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; } = string.Empty;
        public DateTime DataImportacao { get; set; }

        //vinculo com o usuário que realizou a importação
        public string UsuarioId { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!;

        //relacionamento de um para muitos com os contratos importados
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}

