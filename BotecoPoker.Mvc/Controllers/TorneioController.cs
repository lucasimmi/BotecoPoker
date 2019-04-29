using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class TorneioController : Controller
    {
        [Inject]
        public TorneioAplicacao TorneioAplicacao { get; set; }

        public ActionResult FiltroTorneio(PaginacaoModel<Torneio, FiltroTorneio> paginacaoModel)
        {
            return View(TorneioAplicacao.Filtrar(paginacaoModel));
        }

        public ActionResult CadastroTorneio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroTorneio(Torneio torneio)
        {
            var result = TorneioAplicacao.CadastrarTorneio(torneio);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View(torneio);
            }
            return RedirectToAction("FiltroTorneio");
        }

        [HttpPost]
        public ActionResult AlterarTorneio(Torneio torneio)
        {
            var result = TorneioAplicacao.AlterarTorneio(torneio);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View(torneio);
            }
            return RedirectToAction("FiltroTorneio");
        }

        public ActionResult ObterTorneio(int idTorneio)
        {
            return View("AlterarTorneio", TorneioAplicacao.BuscarPorId(idTorneio));
        }
    }
}