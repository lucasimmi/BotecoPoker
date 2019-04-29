using BotecoPoker.Dominio.Entidades;
using System.Collections.Generic;

namespace BotecoPoker.Dominio.modelos
{
    public class PendenciasCliente
    {
        public List<VendaModel> Vendas { get; set; }
        public List<TorneioCliente> TorneiosCliente { get; set; }
        public List<CashGame> CashGames { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public List<ParcelamentoPagamento> Parcelamentos { get; set; }
        public double ValorTotal { get; set; }
        public string NomeCliente { get; set; }
        public long IdCliente { get; set; }
        public double? Saldo { get; set; }
    }
}
