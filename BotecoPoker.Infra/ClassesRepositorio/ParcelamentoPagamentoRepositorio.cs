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
    public class ParcelamentoPagamentoRepositorio : RepositorioBase<ParcelamentoPagamento, long>, IParcelamentoPagamentoRepositorio
    {
        public ParcelamentoPagamentoRepositorio(DbContexto db) : base(db)
        {
        }
    }
}
