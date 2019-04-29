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
    public class ProdutoRepositorio : RepositorioBase<Produto, int>, IProdutoRepositorio
    {
        public ProdutoRepositorio(DbContexto db) : base(db)
        {
        }

        public IEnumerable<SelectListItem> ObterComboProdutos()
        {
            var produtos = Set.ToList();
            if (produtos.Count == 0 || produtos == null)
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
            return produtos.Select(d => new SelectListItem
            {
                Text = d.Nome,
                Value = d.Id.ToString()
            })
          .AsEnumerable();
        }
    }
}
