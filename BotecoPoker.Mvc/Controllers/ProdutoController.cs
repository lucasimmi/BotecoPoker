using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        [Inject]
        public ProdutoAplicacao ProdutoAplicacao { get; set; }

        public ActionResult FiltroProduto(PaginacaoModel<Produto, FiltroProduto> paginacao)
        {
            return View(ProdutoAplicacao.Filtrar(paginacao));
        }

        [HttpGet]
        public ActionResult CadastroProduto()
        {
            return View(new Produto());
        }

        [HttpPost]
        public ActionResult CadastroProduto(Produto entidade)
        {
            var result = ProdutoAplicacao.CadastroProduto(entidade);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View(entidade);
            }
            return RedirectToAction("FiltroProduto");
        }

        public ActionResult BuscarPorId(int id)
        {
            return View("AlterarProduto", ProdutoAplicacao.BuscarPorId(id));
        }

        [HttpPost]
        public ActionResult AlterarProduto(Produto entidade)
        {
            var result = ProdutoAplicacao.AlterarProduto(entidade);
            if(result.TemValor())
            {
                ViewBag.erro = result;
                return View(entidade);
            }
            return RedirectToAction("FiltroProduto");
        }
    }
}