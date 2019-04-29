using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class CashGameAplicacao
    {
        [Inject]
        public ICashGameRepositorio CashGameRepositorio { get; set; }
        [Inject]
        public ValidadorCashGame ValidadorCashGame { get; set; }
        [Inject]
        public IDbContexto Contexto { get; set; }
        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }
        [Inject]
        public IParcelamentoPagamentoRepositorio ParcelamentoPagamentoRepositorio { get; set; }
        [Inject]
        public IPagamentoRepositorio PagamentoRepositorio { get; set; }

        public string CadatrarCashGame(CashGame entidade)
        {
            var result = ValidadorCashGame.Validar(entidade);
            if (result != "")
                return result;
            entidade.DataCadastro = DateTime.Now;
            entidade.IdUsuarioCadastro = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            GeraPagamentoCashGame(entidade);
            CashGameRepositorio.Cadastrar(entidade);
            Contexto.Salvar();
            return result;
        }

        private void GeraPagamentoCashGame(CashGame entidade)
        {
            if (entidade.Situacao == SituacaoVenda.Pago)
            {
                ParcelamentoPagamento parcelamentoPagamento = new ParcelamentoPagamento();
                parcelamentoPagamento.DataPagamento = DateTime.Now;
                parcelamentoPagamento.IdCliente = entidade.IdCliente;
                parcelamentoPagamento.TipoFinalizador = TipoFinalizador.Dinheiro;
                parcelamentoPagamento.ValorPago = entidade.Valor;
                var pagamento = new Pagamento
                {
                    IdCliente = entidade.IdCliente,
                    Data = DateTime.Now,
                    Situacao = SituacaoVenda.Pago,
                    ValorAberto = 0,
                    ValorTotal = entidade.Valor
                };
                parcelamentoPagamento.Pagamento = pagamento;
                PagamentoRepositorio.Cadastrar(pagamento);
                ParcelamentoPagamentoRepositorio.Cadastrar(parcelamentoPagamento);
                entidade.Pagamento = pagamento;
            }
        }

        public string AlterarCashGame(CashGame modelo)
        {
            var entidade = CashGameRepositorio.Buscar(modelo.Id);
            var result = ValidadorCashGame.Validar(entidade);
            if (result != "")
                return result;
            entidade.Valor = modelo.Valor;
            entidade.Situacao = modelo.Situacao;
            entidade.IdCliente = modelo.IdCliente;
            entidade.DataAlteracao = DateTime.Now;
            entidade.IdUsuarioAlteracao = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            CashGameRepositorio.Atualizar(entidade);
            GeraPagamentoCashGame(entidade);
            var row= Contexto.Salvar();
            return result;
        }

        public PaginacaoModel<CashGame, FiltroCashGame> Filtrar(PaginacaoModel<CashGame, FiltroCashGame> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroCashGame(paginacao.Parametro1, paginacao.Parametro2, paginacao.Parametro3);
            else
            {
                paginacao.Parametro1 = paginacao.Filtro.NomeCliente;
                paginacao.Parametro2 = paginacao.Filtro.Valor;
                paginacao.Parametro3 =(short)paginacao.Filtro.Situacao;
            }

            var dataCaixa = CaixaAplicacao.ObterDataCaixaAtivo();
            if (dataCaixa == DateTime.MinValue)
                dataCaixa = DateTime.Now;
            var query = CashGameRepositorio.Query().Where(d => d.DataCadastro >= dataCaixa);

            if (paginacao.Filtro.NomeCliente.TemValor())
                query = query.Where(d => d.Cliente.Nome.Contains(paginacao.Filtro.NomeCliente));
            if (paginacao.Filtro.Valor > 0)
                query = query.Where(d => d.Valor == paginacao.Filtro.Valor);
            if (paginacao.Filtro.Situacao != SituacaoVenda.Nenhum)
                query = query.Where(d => d.Situacao == paginacao.Filtro.Situacao);

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            paginacao.ListaModel.ForEach(d => d.NomeCliente = d?.Cliente?.Nome ?? "");
            return paginacao;
        }

        public CashGame BuscarPorId(int id)
        {
            var cashGame = CashGameRepositorio.Buscar(id);
            cashGame.NomeCliente = cashGame?.Cliente?.Nome;
            return cashGame;
        }
    }
}
