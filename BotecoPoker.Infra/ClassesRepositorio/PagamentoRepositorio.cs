using System.Collections.Generic;
using System.Linq;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class PagamentoRepositorio : RepositorioBase<Pagamento, long>, IPagamentoRepositorio
    {
        public PagamentoRepositorio(DbContexto db) : base(db)
        {
        }

        public List<Pagamento> ObterPagamentosPendentes(long idCliente)
        {
            return Set.Where(d => d.IdCliente == idCliente && d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).ToList();
        }
    }
}
