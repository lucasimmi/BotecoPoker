using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotecoPoker.Dominio.modelos
{
    public class VendaModel
    {
        public long IdVenda { get; set; }
        public double Valor { get; set; }
        public short QtdItem { get; set; }
        public DateTime DataVenda { get; set; }
        public List<PreVenda> PreVendas { get; set; }

        public VendaModel()
        {
        }

        public VendaModel(Venda venda, IPreVendaRepositorio preVendaRepositorio)
        {
            if(venda != null)
            {
                IdVenda = venda.Id;
                Valor = venda.Valor;
                QtdItem = venda.QtdItem;
                DataVenda = venda.DataVenda;
                PreVendas = preVendaRepositorio.Filtrar(d => d.IdVenda == IdVenda).ToList();
            }
        }
    }
}
