using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class TorneioClienteAplicacao
    {
        [Inject]
        public ITorneioClienteRepositorio TorneioClienteRepositorio { get; set; }

        [Inject]
        public ValidadorTorneioCliente Validador { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        [Inject]
        public AutenticacaoAplicacao AutenticacaoAplicacao { get; set; }

        [Inject]
        public CaixaAplicacao CaixaAplicacao { get; set; }

        [Inject]
        public ITorneioRepositorio TorneioRepositorio { get; set; }
        [Inject]
        public PagamentosAplicacao PagamentosAplicacao { get; set; }

        public string CadastrarTorneioCliente(TorneioCliente entidade)
        {
            var result = Validador.ValidaTorneioCliente(entidade);
            if (result != "")
                return result;
            entidade.DataCadastro = DateTime.Now;
            entidade.IdUsuarioCadastro = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            entidade.Situacao = SituacaoVenda.Pendente;
            entidade.ValorPago = entidade.ValorPago ?? 0;
            TorneioClienteRepositorio.Cadastrar(entidade);
            Contexto.Salvar();
            return result;
        }

        public string AlterarTorneioCliente(TorneioCliente entidade)
        {
            var result = Validador.ValidaTorneioCliente(entidade);
            if (result != "")
                return result;
            entidade.DataAlteracao = DateTime.Now;
            entidade.IdUsuarioAlteracao = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            TorneioClienteRepositorio.Atualizar(entidade);
            var rows = Contexto.Salvar();
            return result;
        }

        public TorneioCliente BuscarPorId(long id)
        {
            var torneioCliente = TorneioClienteRepositorio.Buscar(id);
            torneioCliente.NomeCliente = torneioCliente.Cliente.Nome;
            torneioCliente.NomeTorneio = torneioCliente?.Torneio?.Nome ?? "";
            return torneioCliente;
        }

        public void ExcluirTorneio(TorneioCliente entidade)
        {
            TorneioClienteRepositorio.Excluir(entidade);
        }

        public PaginacaoModel<TorneioCliente, FiltroTorneioCliente>
           Filtrar(PaginacaoModel<TorneioCliente, FiltroTorneioCliente> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroTorneioCliente(paginacao.Parametro1, paginacao.Parametro12, paginacao.Parametro13);
            else
            {
                paginacao.Parametro1 = paginacao.Filtro.NomeCliente;
                paginacao.Parametro12 = paginacao.Filtro.CodigoCliente;
                paginacao.Parametro13 = paginacao.Filtro.ApelidoCliente;
            }

            var dataCaixa = CaixaAplicacao.ObterDataCaixaAtivo();
            if (dataCaixa == DateTime.MinValue)
                dataCaixa = DateTime.Now;
            var query = TorneioClienteRepositorio.Query().Where(d => d.DataCadastro >= dataCaixa && d.Situacao != SituacaoVenda.Pago);
            paginacao.TotalJogadores = query.Count();

            if (paginacao.Filtro.CodigoCliente.TemValor())
            {
                query = query.Where(d => d.Cliente.Codigo == paginacao.Filtro.CodigoCliente);
            }
            if (paginacao.Filtro.NomeCliente.TemValor())
            {
                query = query.Where(d => d.Cliente.Nome.Contains(paginacao.Filtro.NomeCliente));
            }
            if (paginacao.Filtro.ApelidoCliente.TemValor())
            {
                query = query.Where(d => d.Cliente.Apelido.Contains(paginacao.Filtro.ApelidoCliente));
            }
            if (paginacao.Letra.TemValor())
            {
                if (!paginacao.Letra.Equals("todos"))
                    query = query.Where(d => d.Cliente.Nome.StartsWith(paginacao.Letra.ToUpper()));
            }
            paginacao.ListaModel = query.OrderBy(d => d.Id).ToList();
            //paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            //paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            paginacao.ListaModel.ForEach(d => PreencheDados(d));
            paginacao.ListaModel.ForEach(d => CalculaValorTotal(d));
            return paginacao;
        }

        public void AtulizaClientesTorneio(List<TorneioCliente> torneioCliente)
        {
            foreach (var clienteModel in torneioCliente)
            {
                var cliente = TorneioClienteRepositorio.Buscar(clienteModel.Id);
                cliente.AtualizaDados(clienteModel);
                TorneioClienteRepositorio.Atualizar(cliente);
                if (clienteModel.Finalizar)
                {
                    PagamentosAplicacao.GeraPagamentoTorneioCliente(clienteModel.IdCliente);
                }
                var row = Contexto.Salvar();
            }
        }



        public void PreencheDados(TorneioCliente torneioCliente)
        {
            torneioCliente.NomeCliente = torneioCliente?.Cliente?.Nome ?? "";
            torneioCliente.NomeTorneio = torneioCliente?.Torneio?.Nome ?? "";
        }

        public void CalculaValorTotal(TorneioCliente torneioCliente)
        {
            torneioCliente.ValorTotal =
            (torneioCliente.Torneio.Addon * torneioCliente?.Addon ?? 0) +
            (torneioCliente.Torneio.BuyIn * torneioCliente?.BuyIn ?? 0) +
            (torneioCliente.Torneio.JackPot * torneioCliente?.JackPot ?? 0) +
            (torneioCliente.Torneio.Jantar * torneioCliente?.Jantar ?? 0) +
            (torneioCliente.Torneio.ReBuy * torneioCliente?.ReBuy ?? 0) +
            (torneioCliente.Torneio.TaxaAdm * torneioCliente?.TaxaAdm ?? 0) +
            (torneioCliente.BonusBeneficente.Contains("5") ? 5 : 0)
            ;
        }
    }
}
