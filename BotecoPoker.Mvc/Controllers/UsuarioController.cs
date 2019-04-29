using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        [Inject]
        public UsuarioAplicacao UsuarioAplicacao { get; set; }


        public ActionResult Index()
        {
            return View(UsuarioAplicacao.ListarUsuarios());
        }

        [HttpPost]
        public ActionResult Gravar(Usuario usuario)
        {
            var erros = UsuarioAplicacao.ValidarUsuario(usuario);
            if (erros.TemValor())
            {
                ViewBag.usuario = usuario;
                ViewBag.erros = erros;
                return View("Index", UsuarioAplicacao.ListarUsuarios());
            }
            UsuarioAplicacao.GravarUsuario(usuario);
            return RedirectToAction("Index");
        }

        public ActionResult DadosUsuarioAtual()
        {
            return View(UsuarioAplicacao.ObterDadosUsuarioLogado());
        }

        [HttpPost]
        public ActionResult Alterar(Usuario usuario)
        {
            string erros = UsuarioAplicacao.ValidarAlteracao(usuario);
            if (erros.TemValor())
            {
                ViewBag.erros = erros;
                return View("DadosUsuarioAtual", usuario);
            }
            UsuarioAplicacao.AlterarUsuario(usuario);
            return RedirectToAction("DadosUsuarioAtual");
        }
    }
}