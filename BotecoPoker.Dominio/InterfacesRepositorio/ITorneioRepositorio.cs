using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface ITorneioRepositorio : IRepositorioBase<Torneio, int>
    {
        IEnumerable<SelectListItem> ObterComboTorneio();
    }
}
