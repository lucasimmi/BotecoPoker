using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.modelos
{
    public class VendaModelo
    {
        public List<PreVenda> PreVendas { get; set; }
        public PaginacaoModel<Cliente, FiltroCliente> PaginacaoCliente { get; set; }
        public double? ValorTotal { get; set; }

        public VendaModelo()
        {
        }
    }
}
