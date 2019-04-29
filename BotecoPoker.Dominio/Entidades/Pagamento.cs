using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Entidades
{
    public class Pagamento : Entidade<long>
    {
        public double ValorTotal { get; set; }
        public double ValorAberto { get; set; }
        public SituacaoVenda Situacao { get; set; }
        public DateTime Data { get; set; }
        public long IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
