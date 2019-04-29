using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario, int>
    {
        Usuario Login(string usuario, string senha);
    }
}
