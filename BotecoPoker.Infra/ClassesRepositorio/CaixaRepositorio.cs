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
    public class CaixaRepositorio : RepositorioBase<Caixa, long>, ICaixaRepositorio
    {
        public CaixaRepositorio(DbContexto db) : base(db)
        {
        }
    }
}
