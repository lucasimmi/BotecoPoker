using System.Collections.Generic;
using System.Linq;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class TorneioClienteRepositorio : RepositorioBase<TorneioCliente, long>, ITorneioClienteRepositorio
    {
        public TorneioClienteRepositorio(DbContexto db) : base(db)
        {
        }

        public List<TorneioCliente> ObterTorneioClientePendente(long idCliente)
        {
            return Set.Where(d => d.IdCliente == idCliente && d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).ToList();
        }
    }
}
