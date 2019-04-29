using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface ICashGameRepositorio : IRepositorioBase<CashGame, int>
    {
        List<CashGame> ObterCashGamesPendente(long idCliente);
    }
}
