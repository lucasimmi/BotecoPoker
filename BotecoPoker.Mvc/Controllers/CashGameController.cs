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
    public class CashGameController : Controller
    {
        [Inject]
        public CashGameAplicacao CashGameAplicacao { get; set; }
        [Inject]
        public ClienteAplicacao ClienteAplicacao { get; set; }
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }

        public ActionResult FiltroCashGame(PaginacaoModel<CashGame, FiltroCashGame> paginacaoModel)
        {
            return View(CashGameAplicacao.Filtrar(paginacaoModel));
        }

        [HttpGet]
        public ActionResult CadastroCashGame()
        {
            var caixaValido = CaixaAplicacao.RetornaCaixaValido();
            if (caixaValido.TemValor())
            {
                ViewBag.caixaValido = caixaValido;
                return View("FiltroCashGame", CashGameAplicacao.Filtrar(new PaginacaoModel<CashGame, FiltroCashGame>()));
            }
            return View(new PaginacaoModel2<CashGame, Cliente, FiltroCliente> { ListaModel = new List<CashGame>(), ListaModel2 = new List<Cliente>() });
        }

        [HttpPost]
        public ActionResult CadastroCashGame(CashGame cashGame)
        {
            var result = CashGameAplicacao.CadatrarCashGame(cashGame);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View("CadastroCashGame", new PaginacaoModel2<CashGame, Cliente, FiltroCliente>
                {
                    ListaModel = new List<CashGame> { cashGame },
                    ListaModel2 = new List<Cliente> { new Cliente
                    {
                        Nome = cashGame.NomeCliente,
                        Id = cashGame.IdCliente}
                    }
                });
            }
            return RedirectToAction("FiltroCashGame");
        }

        public ActionResult ObterCashGame(int idCashGame)
        {
            var paginacaoModel = new PaginacaoModel2<CashGame, Cliente, FiltroCliente>();
            paginacaoModel.ListaModel.Add(CashGameAplicacao.BuscarPorId(idCashGame));
            return View("AlterarCashGame", paginacaoModel);
        }

        [HttpPost]
        public ActionResult AlterarCashGame(CashGame cashGame)
        {
            var result = CashGameAplicacao.AlterarCashGame(cashGame);
            if (result.TemValor())
            {
                ViewBag.erro = result;
                return View("AlterarCashGame", new PaginacaoModel2<CashGame, Cliente, FiltroCliente>
                {
                    ListaModel = new List<CashGame> { cashGame },
                    ListaModel2 = new List<Cliente> { new Cliente
                    {
                        Nome = cashGame.NomeCliente,
                        Id = cashGame.IdCliente}
                    }
                });
            }
            return RedirectToAction("FiltroCashGame");
        }

        public ActionResult FiltrarClienteModal(PaginacaoModel2<CashGame, Cliente, FiltroCliente> paginacao)
        {
            ViewBag.OpenModal = 666;
            var paginacaoClientes = ClienteAplicacao.Filtrar(new PaginacaoModel<Cliente, FiltroCliente>
            {
                Filtro = paginacao.Filtro,
                Pagina = paginacao.Pagina,
                Parametro1 = paginacao.Parametro1,
                Parametro12 = paginacao.Parametro12,
                Parametro13 = paginacao.Parametro13
            });
            return View("CadastroCashGame", new PaginacaoModel2<CashGame, Cliente, FiltroCliente>
            {
                Filtro = paginacaoClientes.Filtro,
                ListaModel2 = paginacaoClientes.ListaModel,
                Pagina = paginacaoClientes.Pagina,
                QtdPaginas = paginacaoClientes.QtdPaginas,
                ListaModel = new List<CashGame>(),
                Parametro1 = paginacaoClientes.Parametro1,
                Parametro12 = paginacaoClientes.Parametro12,
                Parametro13 = paginacaoClientes.Parametro13
            });
        }

        public ActionResult SelecionarCliente(int idCliente)
        {
            PaginacaoModel2<CashGame, Cliente, FiltroCliente> paginacaoModel2 = new PaginacaoModel2<CashGame, Cliente, FiltroCliente>();
            paginacaoModel2.ListaModel2.Add(ClienteAplicacao.BuscarClientePorId(idCliente));
            paginacaoModel2.ListaModel = new List<CashGame>();
            return View("CadastroCashGame", paginacaoModel2);
        }
    }
}