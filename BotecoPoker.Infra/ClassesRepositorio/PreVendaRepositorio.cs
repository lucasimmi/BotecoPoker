using System.Collections.Generic;
using System.Linq;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class PreVendaRepositorio : RepositorioBase<PreVenda, long>, IPreVendaRepositorio
    {
        public PreVendaRepositorio(DbContexto db) : base(db)
        {
        }

        public List<PreVenda> ObterPreVendaAtual(int idUsuarioAtual)
        {
            return Set.Where(d => d.IdUsuario == idUsuarioAtual && d.Ativo == Ativo.Ativo).ToList();
        }
    }
}
