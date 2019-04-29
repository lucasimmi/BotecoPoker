using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Entidades
{
    public class CashGame : Entidade<int>
    {
        public long IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public double Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public SituacaoVenda Situacao { get; set; }
        public string NomeCliente { get; set; }
        public long? IdComprovantePagamento { get; set; }
        public virtual Pagamento Pagamento { get; set; }
    }
}
