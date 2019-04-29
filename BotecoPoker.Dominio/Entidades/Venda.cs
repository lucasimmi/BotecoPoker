using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.Entidades
{
    public class Venda : Entidade<long>
    {
        public double Valor { get; set; }
        public short QtdItem { get; set; }
        public virtual Cliente Cliente { get; set; }
        public long IdCliente { get; set; }
        public DateTime DataVenda { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int IdUsuario { get; set; }
        public SituacaoVenda Situacao { get; set; }
        public long? IdComprovantePagamento { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        public string NomeCliente { get; set; }
    }
}