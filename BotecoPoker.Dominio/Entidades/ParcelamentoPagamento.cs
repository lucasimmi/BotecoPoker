using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.Entidades
{
    public class ParcelamentoPagamento: Entidade<long>
    {
        public DateTime DataPagamento { get; set; }
        public TipoFinalizador TipoFinalizador { get; set; }
        public double ValorPago { get; set; }
        public long? IdPagamento { get; set; }
        public virtual Pagamento Pagamento { get; set; }
        public long? IdCliente { get; set; }
        public double? Saldo { get; set; }
        public bool? TrocoSaldo { get; set; }

    }
}
