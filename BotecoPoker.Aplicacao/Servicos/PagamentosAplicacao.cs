using System;
using System.Collections.Generic;
using System.Linq;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using Ninject;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class PagamentosAplicacao
    {
        [Inject]
        public IClienteRepositorio ClienteRepositorio { get; set; }
        [Inject]
        public IVendaRepositorio VendaRepositorio { get; set; }
        [Inject]
        public IPreVendaRepositorio PreVendaRepositorio { get; set; }
        [Inject]
        public ITorneioClienteRepositorio TorneioClienteRepositorio { get; set; }
        [Inject]
        public ICashGameRepositorio CashGameRepositorio { get; set; }
        [Inject]
        public IPagamentoRepositorio PagamentoRepositorio { get; set; }
        [Inject]
        public IParcelamentoPagamentoRepositorio ParcelamentoPagamentoRepositorio { get; set; }
        [Inject]
        public IDbContexto Contexto { get; set; }
        [Inject]
        public ITorneioRepositorio TorneioRepositorio { get; set; }

        public PaginacaoModel<Cliente, FiltroPagamento> ObterClientesComPendencia(PaginacaoModel<Cliente, FiltroPagamento> filtroPagamento)
        {
            return ClienteRepositorio.ObterClientesComPendencias(filtroPagamento);
        }

        public PendenciasCliente ObterPendenciaCliente(long idCliente)
        {
            var pendencias = new PendenciasCliente
            {
                CashGames = CashGameRepositorio.ObterCashGamesPendente(idCliente),
                TorneiosCliente = ObterItensTorneioModel(TorneioClienteRepositorio.ObterTorneioClientePendente(idCliente)),
                Vendas = VendaRepositorio.ObterVendaModelPendente(idCliente),
                Pagamentos = PagamentoRepositorio.ObterPagamentosPendentes(idCliente)
            };
            pendencias.IdCliente = idCliente;
            var cliente = ClienteRepositorio.Filtrar(d => d.Id == idCliente).Select(d => new { Nome = d.Nome, Saldo = d.Saldo }).FirstOrDefault();
            pendencias.NomeCliente = cliente.Nome;
            pendencias.Saldo = cliente.Saldo;
            pendencias.ValorTotal = pendencias.CashGames.Sum(d => d.Valor);
            pendencias.ValorTotal += pendencias.TorneiosCliente.Sum(d => d.ValorTotal - (d.ValorPago ?? 0));
            pendencias.ValorTotal += pendencias.Vendas.Sum(d => d.Valor);

            return pendencias;
        }

        public bool ExisteOperacaoPendente(DateTime dataCaixa)
        {
            var result = CashGameRepositorio.Filtrar(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente && d.DataCadastro >= dataCaixa).Any();
            if (result)
                return result;
            result = TorneioClienteRepositorio.Filtrar(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente && d.DataCadastro >= dataCaixa).Any();
            if (result)
                return result;
            return VendaRepositorio.Filtrar(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente && d.DataVenda >= dataCaixa).Any();
        }

        public List<TorneioCliente> ObterItensTorneioModel(List<TorneioCliente> torneiosCliente)
        {
            if (torneiosCliente != null && torneiosCliente.Any())
            {
                foreach (var torneioCliente in torneiosCliente)
                {
                    var torneio = TorneioRepositorio.Buscar(torneioCliente.IdTorneio);

                    double valorTotal = 0;
                    torneioCliente.ItensTorneio = new List<ItemTorneio>();
                    if (torneioCliente.JackPot != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "JackPot", ValorItem = ((double)torneio.JackPot).ToString("c2"), QtdItem = (short)torneioCliente.JackPot });
                        valorTotal += (short)torneioCliente.JackPot * (double)torneio.JackPot;
                    }
                    if (torneioCliente.Addon != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Addon", QtdItem = (short)torneioCliente.Addon, ValorItem = ((double)torneio.Addon).ToString("c2") });
                        valorTotal += (short)torneioCliente.Addon * (double)torneio.Addon;
                    }
                    if (torneioCliente.BonusBeneficente != null && torneioCliente.BonusBeneficente != "0")
                    {
                        if (torneioCliente.BonusBeneficente.Contains("5"))
                        {
                            torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Bônus Beneficente", QtdItem = 1, ValorItem = "5" });
                            valorTotal += 5;
                        }
                        else
                            torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Bônus Beneficente", QtdItem = 1, ValorItem = "Alimento" });
                    }
                    if (torneioCliente.BuyIn != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Buy-In", QtdItem = (short)torneioCliente.BuyIn, ValorItem = ((double)torneio.BuyIn).ToString("c2") });
                        valorTotal += (short)torneioCliente.BuyIn * (double)torneio.BuyIn;
                    }
                    if (torneioCliente.Jantar != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Jantar", QtdItem = (short)torneioCliente.Jantar, ValorItem = ((double)torneio.Jantar).ToString("c2") });
                        valorTotal += (short)torneioCliente.Jantar * (double)torneio.Jantar;
                    }
                    if (torneioCliente.ReBuy != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Re-Buy", QtdItem = (short)torneioCliente.ReBuy, ValorItem = ((double)torneio.ReBuy).ToString("c2") });
                        valorTotal += (short)torneioCliente.ReBuy * (double)torneio.ReBuy;
                    }
                    if (torneioCliente.TaxaAdm != null)
                    {
                        torneioCliente.ItensTorneio.Add(new ItemTorneio { NomeItem = "Taxa Administrador", QtdItem = (short)torneioCliente.TaxaAdm, ValorItem = ((double)torneio.TaxaAdm).ToString("c2") });
                        valorTotal += (short)torneioCliente.TaxaAdm * (double)torneio.TaxaAdm;
                    }
                    torneioCliente.ValorTotal = valorTotal;
                    torneioCliente.NomeTorneio = torneio.Nome;
                }
            }
            return torneiosCliente;
        }

        public void GeraPagamentoTorneioCliente(long idCliente)
        {
            var torneiosCliente = ObterItensTorneioModel(TorneioClienteRepositorio.ObterTorneioClientePendente(idCliente));
            Pagamento pagamento = null;
            //quita se valor for igual
            if (torneiosCliente.Sum(d => d.ValorTotal) == torneiosCliente.Sum(d => d.ValorPago))
            {
                pagamento = new Pagamento
                {
                    Data = DateTime.Now,
                    IdCliente = idCliente,
                    ValorAberto = 0,
                    ValorTotal = torneiosCliente.Sum(d => d.ValorTotal),
                    Situacao = SituacaoVenda.Pago
                };
            }
            //deixa pendente valor faltante
            else if (torneiosCliente.Sum(d => d.ValorTotal) > torneiosCliente.Sum(d => d.ValorPago))
            {
                pagamento = new Pagamento
                {
                    Data = DateTime.Now,
                    IdCliente = idCliente,
                    ValorAberto = torneiosCliente.Sum(d => d.ValorTotal) - torneiosCliente.Sum(d => d.ValorPago ?? 0),
                    ValorTotal = torneiosCliente.Sum(d => d.ValorTotal),
                    Situacao = SituacaoVenda.Pendente
                };
            }
            //deixa como saldo valor superior
            else if (torneiosCliente.Sum(d => d.ValorTotal) < torneiosCliente.Sum(d => d.ValorPago))
            {
                pagamento = new Pagamento
                {
                    Data = DateTime.Now,
                    IdCliente = idCliente,
                    ValorAberto = 0,
                    ValorTotal = torneiosCliente.Sum(d => d.ValorTotal),
                    Situacao = SituacaoVenda.Pago
                };
                AtualizaSaldoCliente(idCliente, torneiosCliente.Sum(d => d.ValorPago ?? 0) - torneiosCliente.Sum(d => d.ValorTotal));
            }

            PagamentoRepositorio.Cadastrar(pagamento);
            AtualizaPendencias(new PendenciasCliente { TorneiosCliente = torneiosCliente, IdCliente = idCliente }, pagamento);
        }

        private void AtualizaSaldoCliente(long idCliente, double saldo)
        {
            var cliente = ClienteRepositorio.Buscar(idCliente);
            cliente.Saldo = saldo;
            ClienteRepositorio.Atualizar(cliente);
        }

        public PendenciasCliente GerarPagamento(ParcelamentoPagamento parcelamentoPagamento)
        {
            parcelamentoPagamento.ValorPago += (parcelamentoPagamento.Saldo ?? 0);
            Pagamento pagamento = null;
            if (parcelamentoPagamento.IdPagamento != null)
            {
                pagamento = PagamentoRepositorio.Buscar((long)parcelamentoPagamento.IdPagamento);
                if (pagamento.ValorAberto - parcelamentoPagamento.ValorPago <= 0)
                {
                    pagamento.ValorAberto = 0;
                    pagamento.Situacao = Dominio.Enumeradores.SituacaoVenda.Pago;
                    pagamento.Data = DateTime.Now;
                }
                else
                {
                    pagamento.ValorAberto = pagamento.ValorAberto - parcelamentoPagamento.ValorPago;
                    pagamento.Situacao = Dominio.Enumeradores.SituacaoVenda.Pendente;
                }
                AtualizaSaldoCliente(pagamento.IdCliente, parcelamentoPagamento.TrocoSaldo, parcelamentoPagamento.Saldo, pagamento);
                PagamentoRepositorio.Atualizar(pagamento);
                GerarParcelamentoPagamento(parcelamentoPagamento, pagamento);
                var result = Contexto.Salvar();
            }
            else
            {
                pagamento = new Pagamento
                {
                    Data = DateTime.Now,
                    IdCliente = (long)parcelamentoPagamento.IdCliente
                };

                var pendencias = ObterPendenciaCliente((long)parcelamentoPagamento.IdCliente);
                if (pendencias != null)
                {
                    string troco = "";
                    var valorAntigo = pendencias.ValorTotal;
                    pagamento.ValorTotal = pendencias.ValorTotal + pendencias.TorneiosCliente.Sum(d => d.ValorPago ?? 0);
                    if (valorAntigo - parcelamentoPagamento.ValorPago <= 0)
                    {
                        pagamento.ValorAberto = 0;
                        pagamento.Situacao = Dominio.Enumeradores.SituacaoVenda.Pago;
                        troco = (valorAntigo - parcelamentoPagamento.ValorPago).ToString("C2");
                    }
                    else
                    {
                        pagamento.ValorAberto = pendencias.ValorTotal - parcelamentoPagamento.ValorPago;
                        pagamento.Situacao = Dominio.Enumeradores.SituacaoVenda.Pendente;
                    }
                    if (parcelamentoPagamento.ValorPago - valorAntigo > 0)
                        parcelamentoPagamento.Saldo = parcelamentoPagamento.ValorPago - valorAntigo;
                    AtualizaSaldoCliente(pagamento.IdCliente, parcelamentoPagamento.TrocoSaldo, parcelamentoPagamento.Saldo, pagamento);
                    PagamentoRepositorio.Cadastrar(pagamento);
                    GerarParcelamentoPagamento(parcelamentoPagamento, pagamento);
                    AtualizaPendencias(pendencias, pagamento);
                    Contexto.Salvar();
                }
            }
            return ObterPendenciaCliente((long)parcelamentoPagamento.IdCliente);
        }

        private void AtualizaSaldoCliente(long idCliente, bool? TrocoSaldo, double? valorSaldo, Pagamento pagamento)
        {
            var cliente = ClienteRepositorio.Buscar(idCliente);
            if ((TrocoSaldo ?? false) && (valorSaldo ?? 0) > 0 && pagamento.Situacao== SituacaoVenda.Pago)
            {
                cliente.Saldo = valorSaldo;
            }
            else
            {
                cliente.Saldo = 0;
            }
            ClienteRepositorio.Atualizar(cliente);
        }

        public string ValidaPagamento(ParcelamentoPagamento parcelamentoPagamento)
        {
            var pagamento = PagamentoRepositorio.Buscar(parcelamentoPagamento.IdPagamento ?? 0);
            if (pagamento != null && pagamento.Situacao == SituacaoVenda.Pendente && parcelamentoPagamento.TipoFinalizador == TipoFinalizador.Aberto)
                return "Venda já está em aberto na conta do cliente";
            if (parcelamentoPagamento.TipoFinalizador != TipoFinalizador.Aberto && parcelamentoPagamento.ValorPago + parcelamentoPagamento.Saldo == 0)
                return "Favor preencher o valor a ser pago";
            return "";
        }

        public void GerarParcelamentoPagamento(ParcelamentoPagamento parcelamentoPagamento, Pagamento pagamento)
        {
            parcelamentoPagamento.DataPagamento = DateTime.Now;
            parcelamentoPagamento.Pagamento = pagamento;
            parcelamentoPagamento.TipoFinalizador = parcelamentoPagamento.TipoFinalizador;
            parcelamentoPagamento.ValorPago = parcelamentoPagamento.ValorPago;
            ParcelamentoPagamentoRepositorio.Cadastrar(parcelamentoPagamento);
        }

        public void AtualizaPendencias(PendenciasCliente pendenciasCliente, Pagamento pagamento)
        {
            if (pendenciasCliente.CashGames != null)
                foreach (var cash in pendenciasCliente.CashGames)
                {
                    cash.Situacao = Dominio.Enumeradores.SituacaoVenda.Pago;
                    cash.Pagamento = pagamento;
                    CashGameRepositorio.Atualizar(cash);
                }
            if (pendenciasCliente.TorneiosCliente != null)
                foreach (var torneio in pendenciasCliente.TorneiosCliente)
                {
                    torneio.Situacao = Dominio.Enumeradores.SituacaoVenda.Pago;
                    torneio.Pagamento = pagamento;
                    TorneioClienteRepositorio.Atualizar(torneio);
                }
            if (pendenciasCliente.Vendas != null)
                foreach (var vendaModel in pendenciasCliente.Vendas)
                {
                    var venda = VendaRepositorio.Buscar(vendaModel.IdVenda);
                    venda.Situacao = Dominio.Enumeradores.SituacaoVenda.Pago;
                    venda.Pagamento = pagamento;
                    VendaRepositorio.Atualizar(venda);
                }
        }
    }
}
