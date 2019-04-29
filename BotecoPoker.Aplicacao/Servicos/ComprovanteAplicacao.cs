using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class ComprovanteAplicacao
    {
        [Inject]
        public IPagamentoRepositorio PagamentoRepositorio { get; set; }
        [Inject]
        public ICashGameRepositorio CashGameRepositorio { get; set; }
        [Inject]
        public ITorneioClienteRepositorio TorneiosClienteRepositorio { get; set; }
        [Inject]
        public IVendaRepositorio VendaRepositorio { get; set; }
        [Inject]
        public IPreVendaRepositorio PreVendaRepositorio { get; set; }
        [Inject]
        public PagamentosAplicacao PagamentosAplicacao { get; set; }
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }
        [Inject]
        public IParcelamentoPagamentoRepositorio ParcelamentoPagamentoRepositorio { get; set; }


        public PaginacaoModel<Pagamento, FiltroPagamento> FiltrarComprovante
            (PaginacaoModel<Pagamento, FiltroPagamento> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroPagamento(paginacao.Parametro1, paginacao.Parametro13, paginacao.Parametro4, paginacao.ParameterBool);
            else
            {
                paginacao.Parametro1 = paginacao.Filtro.NomeCliente;
                paginacao.Parametro13 = paginacao.Filtro.ApelidoCliente;
                paginacao.Parametro4 = paginacao.Filtro.DataPagamento;
                paginacao.ParameterBool = paginacao.Filtro.Tudo;

            }
            var query = PagamentoRepositorio.Query().Where(d => d.ValorTotal != 0 && d.Situacao != Dominio.Enumeradores.SituacaoVenda.Pendente);
            if (paginacao.Filtro.NomeCliente.TemValor())
                query = query.Where(d => d.Cliente.Nome.Contains(paginacao.Filtro.NomeCliente));
            if (!paginacao?.Filtro?.Tudo ?? true)
            {
                if (paginacao.Filtro.DataPagamento.HasValue)
                    query = query.Where(d => DbFunctions.TruncateTime(d.Data) == paginacao.Filtro.DataPagamento);
                else
                {
                    var dataCaixaAtivo = CaixaAplicacao.ObterDataCaixaAtivo();
                    query = query.Where(d => d.Data >= dataCaixaAtivo);
                }
            }
            if (paginacao.Filtro.ApelidoCliente.TemValor())
                query = query.Where(d => d.Cliente.Apelido.Contains(paginacao.Filtro.ApelidoCliente));

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            return paginacao;
        }

        public PendenciasCliente ObterDetalhesPagamento(long idPagamento)
        {
            var detalhesPagamento = new PendenciasCliente();
            detalhesPagamento.CashGames = CashGameRepositorio.Filtrar(d => d.IdComprovantePagamento == idPagamento).ToList();
            detalhesPagamento.TorneiosCliente = PagamentosAplicacao.ObterItensTorneioModel(TorneiosClienteRepositorio.Filtrar(d => d.IdComprovantePagamento == idPagamento).ToList());
            var idNomeCliente = PagamentoRepositorio.Filtrar(d => d.Id == idPagamento).Select(d => new { Nome = d.Cliente.Nome, Id = d.IdCliente, Saldo = d.Cliente.Saldo}).FirstOrDefault();
            detalhesPagamento.IdCliente = idNomeCliente.Id;
            detalhesPagamento.NomeCliente = idNomeCliente.Nome;
            detalhesPagamento.Saldo = idNomeCliente.Saldo;
            var vendas = VendaRepositorio.Filtrar(d => d.IdComprovantePagamento == idPagamento).ToList();
            detalhesPagamento.Vendas = new List<VendaModel>();
            detalhesPagamento.Parcelamentos = ParcelamentoPagamentoRepositorio.Filtrar(d => d.IdPagamento == idPagamento).ToList();
            if (detalhesPagamento.Parcelamentos.Count() == 1)
                detalhesPagamento.Parcelamentos = new List<ParcelamentoPagamento>();
            foreach (var venda in vendas)
            {
                detalhesPagamento.Vendas.Add(new VendaModel(venda, PreVendaRepositorio));
            }
            return detalhesPagamento;
        }
    }
}
