using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IClienteRepositorio : IRepositorioBase<Cliente, long>
    {
        Cliente ObterPorCodigo(string codigo);

        PaginacaoModel<Cliente, FiltroPagamento> ObterClientesComPendencias(PaginacaoModel<Cliente, FiltroPagamento> paginacaoModel);
    }
}
