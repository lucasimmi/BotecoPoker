using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class TorneioClienteController : Controller
    {
        [Inject]
        public TorneioClienteAplicacao TorneioClienteAplicacao { get; set; }
        [Inject]
        public TorneioAplicacao TorneioAplicacao { get; set; }
        [Inject]
        public ClienteAplicacao ClienteAplicacao { get; set; }
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }

        public ActionResult FiltroTorneioCliente(PaginacaoModel<TorneioCliente, FiltroTorneioCliente> paginacaoModel, string letra = "")
        {
            if (paginacaoModel == null)
                paginacaoModel = new PaginacaoModel<TorneioCliente, FiltroTorneioCliente>();
            paginacaoModel.Letra = letra;
            return View("FiltroTorneioCliente", TorneioClienteAplicacao.Filtrar(paginacaoModel));
        }

        public ActionResult CadastroTorneioCliente()
        {
            var caixaValido = CaixaAplicacao.RetornaCaixaValido();
            if (caixaValido.TemValor())
            {
                ViewBag.caixaValido = caixaValido;
                return View("FiltroTorneioCliente", TorneioClienteAplicacao.Filtrar(new PaginacaoModel<TorneioCliente, FiltroTorneioCliente>()));
            }
            CarregarComboTorneios();
            return View(new PaginacaoModel<Cliente, FiltroCliente>());
        }

        [HttpPost]
        public ActionResult CadastroTorneioCliente(TorneioCliente entidade)
        {
            var result = TorneioClienteAplicacao.CadastrarTorneioCliente(entidade);
            if (result.TemValor())
            {
                var clienteSelecionado = ClienteAplicacao.ClienteRepositorio.Buscar(entidade.IdCliente);
                CarregarComboTorneios();
                ViewBag.erro = result;
                return View(new PaginacaoModel<Cliente, FiltroCliente>() { ListaModel = new List<Cliente> { clienteSelecionado } });
            }
            return RedirectToAction("FiltroTorneioCliente");
        }

        public ActionResult FiltrarClienteModal(PaginacaoModel<Cliente, FiltroCliente> paginacaoModel)
        {
            CarregarComboTorneios();
            ViewBag.OpenModal = 666;
            return View("CadastroTorneioCliente", ClienteAplicacao.Filtrar(paginacaoModel));
        }

        public ActionResult SelecionarCliente(int idCliente)
        {
            CarregarComboTorneios();
            PaginacaoModel<Cliente, FiltroCliente> paginacaoCliente = new PaginacaoModel<Cliente, FiltroCliente>();
            paginacaoCliente.ListaModel.Add(ClienteAplicacao.BuscarClientePorId(idCliente));
            return View("CadastroTorneioCliente", paginacaoCliente);
        }



        public ActionResult ObterTorneioCliente(long idTorneioCliente)
        {
            return View("AlterarTorneioCliente", TorneioClienteAplicacao.BuscarPorId(idTorneioCliente));
        }

        public ActionResult AlterarTorneioCliente(TorneioCliente torneioCliente)
        {
            var result = TorneioClienteAplicacao.AlterarTorneioCliente(torneioCliente);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View(torneioCliente);
            }
            return RedirectToAction("FiltroTorneioCliente");
        }

        public void CarregarComboTorneios()
        {
            ViewBag.ComboTorneios = TorneioAplicacao.ObterComboTorneio();
        }

        public ActionResult ObterClientesDoTorneio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AtualizarClientesTorneio(PaginacaoModel<TorneioCliente, FiltroTorneioCliente> paginacaoModel)
        {
            if (paginacaoModel?.ListaModel != null)
            {
                TorneioClienteAplicacao.AtulizaClientesTorneio(paginacaoModel.ListaModel);
            }
            return RedirectToAction("FiltroTorneioCliente");
        }
    }
}