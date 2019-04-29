using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class TorneioRepositorio : RepositorioBase<Torneio, int>, ITorneioRepositorio
    {
        public TorneioRepositorio(DbContexto db) : base(db)
        {
        }

        public IEnumerable<SelectListItem> ObterComboTorneio()
        {
            var torneios = Set
               .Where(d => d.Ativo == Dominio.Enumeradores.Ativo.Ativo)
               .ToList();

            if (torneios.Count == 0)
            {
                var nenhum = new List<SelectListItem>
                {
                    new SelectListItem()
                    {
                        Text = "Nenhum",
                        Value = "0"
                    }
                };
                return nenhum;
            }

           return torneios.Select(d => new SelectListItem
            {
                Text = d.Nome,
                Value = d.Id.ToString()
            })
            .AsEnumerable();
        }
    }
}
