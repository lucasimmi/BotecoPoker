using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        [Inject]
        public ClienteAplicacao ClienteAplicacao { get; set; }

        public ActionResult FiltroCliente(PaginacaoModel<Cliente, FiltroCliente> paginacaoModel)
        {
            return View(ClienteAplicacao.Filtrar(paginacaoModel));
        }

        [HttpGet]
        public ActionResult CadastroCliente()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public ActionResult CadastroCliente(Cliente Cliente)
        {
            var result = ClienteAplicacao.CadastrarCliente(Cliente);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View(Cliente);
            }
            return RedirectToAction("FiltroCliente");
        }

        public ActionResult BuscarCliente()
        {
            return View();
        }

        public ActionResult AlterarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarCliente(Cliente Cliente)
         {
            var result = ClienteAplicacao.AtualizarCliente(Cliente);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View("AlterarCliente", Cliente);
            }
            return RedirectToAction("FiltroCliente");
        }

        public ActionResult BuscarPorId(int Id)
        {
            return View("AlterarCliente", ClienteAplicacao.BuscarClientePorId(Id));
        }
    }
}