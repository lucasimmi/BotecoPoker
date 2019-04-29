using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IPreVendaRepositorio : IRepositorioBase<PreVenda, long>
    {
        List<PreVenda> ObterPreVendaAtual(int idUsuarioAtual);
    }
}
