using BotecoPoker.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.Modelos
{
    public class FinalizacaoVendaModel
    {
        public long IdCliente { get; set; }
        public double? ValorPago { get; set; }
        public bool? TrocoSaldo { get; set; }
        public TipoFinalizador? TipoFinalizador { get; set; }
    }
}
