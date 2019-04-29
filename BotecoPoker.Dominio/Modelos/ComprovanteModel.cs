using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class ComprovanteModel
    {
        public long IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public VendaModel VendaModel { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}
