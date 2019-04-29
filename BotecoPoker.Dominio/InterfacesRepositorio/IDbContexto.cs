using BotecoPoker.Dominio.Entidades;
using Ninject;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IDbContexto : IDisposable
    {
        int Salvar();
        void FecharConexao();
    }
}
