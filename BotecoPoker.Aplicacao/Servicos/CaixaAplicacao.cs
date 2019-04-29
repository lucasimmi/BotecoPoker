using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.ClassesRepositorio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class CaixaAplicacao
    {
        [Inject]
        public ICaixaRepositorio CaixaRepositorio { get; set; }

        [Inject]
        public AutenticacaoAplicacao AutenticacaoAplicacao { get; set; }

        [Inject]
        public PagamentosAplicacao PagamentosAplicacao { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        [Inject]
        public ICashGameRepositorio CashGameRepositorio { get; set; }

        [Inject]
        public ITorneioClienteRepositorio TorneioClienteRepositorio { get; set; }

        [Inject]
        public ITorneioRepositorio TorneioRepositorio { get; set; }

        [Inject]
        public IVendaRepositorio VendaRepositorio { get; set; }

        public Caixa AbrirCaixa()
        {
            var caixa = new Caixa
            {
                DataAbertura = DateTime.Now,
                IdUsuarioAbertura = AutenticacaoAplicacao.ObterUsuarioLogado().Id,
                Ativo = Ativo.Ativo
            };
            CaixaRepositorio.Cadastrar(caixa);
            Contexto.Salvar();
            return LoadCaixa();
        }

        public string FecharCaixa()
        {
            var caixa = CaixaRepositorio.Filtrar(d => d.Ativo == Ativo.Ativo).FirstOrDefault();
            var result = PagamentosAplicacao.ExisteOperacaoPendente(caixa.DataAbertura);
            if (result)
                return "Caixa só poderá ser fechado quando não houver pagamentos pendentes!";

            if (caixa != null)
            {
                caixa.Ativo = Ativo.Inativo;
                caixa.DataFechamento = DateTime.Now;
                caixa.IdUsuarioFechamento = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
                CaixaRepositorio.Atualizar(caixa);
                var resultado = Contexto.Salvar();
            }
            return "";
        }

        public Caixa LoadCaixa()
        {
            var caixa = CaixaRepositorio.Filtrar(d => d.Ativo == Ativo.Ativo).FirstOrDefault();

            if (caixa?.Ativo == Ativo.Ativo)
            {
                caixa.ValorCashGame = ObterValorAtualCash(caixa.DataAbertura);
                caixa.ValorTorneios = ObterValorAtualTorneio(caixa.DataAbertura);
                caixa.ValorVenda = ObterValorAtualVenda(caixa.DataAbertura);
            }
            return caixa;
        }

        private double? ObterValorAtualCash(DateTime dataCaixa)
        {
            if (CashGameRepositorio.Filtrar(d => d.DataCadastro >= dataCaixa).Any())
                return CashGameRepositorio.Filtrar(d => d.DataCadastro >= dataCaixa).Sum(d => d.Valor);
            return 0;
        }

        private Dictionary<string, double> ObterValorAtualTorneio(DateTime dataCaixa)
        {
            var torneiosAtivos = TorneioRepositorio.Filtrar(d => d.Ativo == Ativo.Ativo).ToList();
            Dictionary<string, double> valorTorneios = new Dictionary<string, double>();
            foreach (var torneio in torneiosAtivos)
            {
                var entidades = TorneioClienteRepositorio.Filtrar(d => d.DataCadastro >= dataCaixa && d.IdTorneio == torneio.Id).ToList();
                double valorTotal = 0;
                foreach (var entidade in entidades)
                {
                    valorTotal += entidade.JackPot * torneio.JackPot ?? 0;
                    valorTotal += entidade.Jantar * torneio.Jantar ?? 0;
                    valorTotal += entidade.ReBuy * torneio.ReBuy ?? 0;
                    valorTotal += entidade.TaxaAdm * torneio.TaxaAdm ?? 0;
                    valorTotal += entidade.BuyIn * torneio.BuyIn ?? 0;
                    valorTotal += entidade.Addon * torneio.Addon ?? 0;
                }
                valorTorneios.Add(torneio.Nome, valorTotal);
            }
            return valorTorneios;
        }

        private double? ObterValorAtualVenda(DateTime dataCaixa)
        {
            if (VendaRepositorio.Filtrar(d => d.DataVenda >= dataCaixa).Any())
                return VendaRepositorio.Filtrar(d => d.DataVenda >= dataCaixa).Sum(d => d.Valor);
            return 0;
        }

        public string RetornaCaixaValido()
        {
            var dataMaxima = DateTime.Now.AddDays(1).AddHours(10.00);
            var caixaAberto = CaixaRepositorio.Filtrar(d => d.Ativo == Ativo.Ativo).FirstOrDefault();
            if (caixaAberto == null || caixaAberto.DataAbertura.AddDays(1) < DateTime.Now)
                return "Favor reabrir o caixa!";
            return "";
        }

        public DateTime ObterDataCaixaAtivo()
        {
            return CaixaRepositorio.Filtrar(d => d.Ativo == Ativo.Ativo).Select(d => d.DataAbertura).FirstOrDefault();
        }
    }
}
