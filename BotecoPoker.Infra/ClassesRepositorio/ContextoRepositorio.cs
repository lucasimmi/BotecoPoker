using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.Utils;
using BotecoPoker.Infra.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class ContextoRepositorio : RepositorioBase<Entidade<object>, object>, IDbContexto
    {
        public ContextoRepositorio(DbContexto db) : base(db)
        {
        }

        public void Dispose() => Db.Dispose();

        public int Salvar() => Db.Salvar();

        public void FecharConexao() => Db.FecharConexao();
    }
}
