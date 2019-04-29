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
    public class ItemVendaRepositorio : RepositorioBase<ItemVenda, long>, IItemVendaRepositorio
    {
        public ItemVendaRepositorio(DbContexto db) : base(db)
        {
        }
    }
}
