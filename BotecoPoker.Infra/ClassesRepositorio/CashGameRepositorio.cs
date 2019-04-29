using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class CashGameRepositorio : RepositorioBase<CashGame, int>, ICashGameRepositorio
    {
        public CashGameRepositorio(DbContexto db) : base(db)
        {
        }

        public List<CashGame> ObterCashGamesPendente(long idCliente)
        {
            return Set.Where(d => d.IdCliente == idCliente && d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).ToList();
        }
    }
}
