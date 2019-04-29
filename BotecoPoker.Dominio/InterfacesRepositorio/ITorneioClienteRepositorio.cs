using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface ITorneioClienteRepositorio : IRepositorioBase<TorneioCliente, long>
    {
        List<TorneioCliente> ObterTorneioClientePendente(long idCliente);
    }
}
