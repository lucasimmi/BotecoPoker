using BotecoPoker.Aplicacao.Servicos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login");
        }

        public ActionResult TelaInicial()
        {
            return View("Index", CaixaAplicacao.LoadCaixa());
        }

        public ActionResult AbrirCaixa()
        {
            return View("Index", CaixaAplicacao.AbrirCaixa());
        }

        public ActionResult FecharCaixa()
        {
            var result = CaixaAplicacao.FecharCaixa();
            if(result != "") {
                ViewBag.erro = result;
                return View("Index", CaixaAplicacao.LoadCaixa());
            }
            return RedirectToAction("TelaInicial");
        }
    }
}