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
    public class UsuarioRepositorio : RepositorioBase<Usuario, int>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(DbContexto db) : base(db)
        {
        }

        public Usuario Login(string usuario, string senha) =>
           Set.FirstOrDefault(d => d.Login == usuario && d.Senha == senha);
    }
}
