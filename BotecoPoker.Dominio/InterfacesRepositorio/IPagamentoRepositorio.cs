using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IPagamentoRepositorio : IRepositorioBase<Pagamento, long>
    {
        List<Pagamento> ObterPagamentosPendentes(long idCliente);
    }
}
