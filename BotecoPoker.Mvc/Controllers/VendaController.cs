using BotecoPoker.Aplicacao.Servicos;
using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.modelos;
using BotecoPoker.Dominio.Modelos;
using Ninject;
using System.Web.Mvc;

namespace BotecoPoker.Mvc.Controllers
{
    [Authorize]
    public class VendaController : Controller
    {
        [Inject]
        public VendaAplicacao VendaAplicacao { get; set; }
        [Inject]
        public ProdutoAplicacao ProdutoAplicacao { get; set; }
        [Inject]
        public ClienteAplicacao ClienteAplicacao { get; set; }
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }

        public ActionResult FiltroVenda(PaginacaoModel2<Venda, PreVenda, FiltroVenda> paginacaoModel)
        {
            return View(VendaAplicacao.FiltrarVenda(paginacaoModel));
        }

        public ActionResult ObterDetalhesVenda(long idVenda)
        {
            var paginacaoModel = VendaAplicacao.FiltrarVenda(new PaginacaoModel2<Venda, PreVenda, FiltroVenda>());
            ViewBag.OpenModal = 666;
            return View("FiltroVenda", VendaAplicacao.ObterDetalhesVenda(paginacaoModel, idVenda));
        }

        [HttpPost]
        public ActionResult FinalizarVenda(FinalizacaoVendaModel finalizacaoVendaModel)
        {
            var resultado = VendaAplicacao.FinalizarVenda(finalizacaoVendaModel);
            if (resultado.TemValor())
            {
                ViewBag.erro = resultado;
                var preVendaModelo = VendaAplicacao.ObterPreVendaAtual();
                preVendaModelo.PaginacaoCliente.ListaModel.Add(ClienteAplicacao.BuscarClientePorId(finalizacaoVendaModel.IdCliente));
                return View("IncluirVenda", preVendaModelo);
            }
            return View("FiltroVenda", VendaAplicacao.FiltrarVenda(new PaginacaoModel2<Venda, PreVenda, FiltroVenda>()));
        }

        public ActionResult DeletarVenda(long idVenda)
        {
            ViewBag.erro = VendaAplicacao.DeletarVenda(idVenda);
            return View("FiltroVenda", VendaAplicacao.FiltrarVenda(new PaginacaoModel2<Venda, PreVenda, FiltroVenda>()));
        }

        public ActionResult IncluirVenda()
        {
            var caixaValido = CaixaAplicacao.RetornaCaixaValido();
            if (caixaValido.TemValor())
            {
                ViewBag.erro = caixaValido;
                return View("FiltroVenda", VendaAplicacao.FiltrarVenda(new PaginacaoModel2<Venda, PreVenda, FiltroVenda>()));
            }
            CarregarBag();
            return View("IncluirVenda", VendaAplicacao.ObterPreVendaAtual());
        }

        [HttpPost]
        public ActionResult IncluirVenda(PreVenda preVenda)
        {
            CarregarBag();
            ViewBag.erro = VendaAplicacao.GravarPreVenda(preVenda);
            return View("IncluirVenda", VendaAplicacao.ObterPreVendaAtual());
        }

        private void CarregarBag()
        {
            ViewBag.ComboProdutos = ProdutoAplicacao.ComboProduto();
        }

        public ActionResult SelecionarClienteVenda(int idCliente)
        {
            CarregarBag();
            PaginacaoModel<Cliente, FiltroCliente> paginacaoCliente = new PaginacaoModel<Cliente, FiltroCliente>();
            paginacaoCliente.ListaModel.Add(ClienteAplicacao.BuscarClientePorId(idCliente));
            var preVendaModelo = VendaAplicacao.ObterPreVendaAtual();
            preVendaModelo.PaginacaoCliente = paginacaoCliente;
            return View("IncluirVenda", preVendaModelo);
        }

        public ActionResult FiltrarClienteModal(PaginacaoModel<Cliente,FiltroCliente> paginacaoModel)
        {
            CarregarBag();
            ViewBag.OpenModal = 666;
            var clienteFiltrados = ClienteAplicacao.Filtrar(paginacaoModel);
            var preVendaModelo = VendaAplicacao.ObterPreVendaAtual();
            preVendaModelo.PaginacaoCliente = clienteFiltrados;
            return View("IncluirVenda", preVendaModelo);
        }

        public ActionResult DeletarPreVenda(long idPreVenda)
        {
            CarregarBag();
            var erro = VendaAplicacao.DeletarPreVenda(idPreVenda);
            ViewBag.erro = erro;
            return View("IncluirVenda", VendaAplicacao.ObterPreVendaAtual());
        }

    }
}