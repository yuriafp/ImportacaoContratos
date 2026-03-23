using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportacaoContratos.Domain.Entities
{
    public class Usuario
    {
        public int id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        //relacionamento de um para muitos com as importações realizadas por esse usuário
        public ICollection<Importacao> Importacoes { get; set; } = new List<Importacao>();
    }
}
