using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IVendaRepositorio : IRepositorioBase<Venda, long>
    {
        List<VendaModel> ObterVendaModelPendente(long idCliente);
    }
}
