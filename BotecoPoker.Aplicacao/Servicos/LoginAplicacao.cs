using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class LoginAplicacao
    {
        [Inject]
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public Usuario Login(string usuario, string senha)
        {
            return UsuarioRepositorio.Login(usuario, senha);
        }
    }
}
