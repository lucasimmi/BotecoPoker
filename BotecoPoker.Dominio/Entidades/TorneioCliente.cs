using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.modelos;
using BotecoPoker.Dominio.Utils;
using System;
using System.Collections.Generic;

namespace BotecoPoker.Dominio.Entidades
{
    public class TorneioCliente : Entidade<long>
    {
        public int IdTorneio { get; set; }
        public virtual Torneio Torneio { get; set; }
        public long IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
        public string BonusBeneficente { get; set; }
        public short? BuyIn { get; set; }
        public short? ReBuy { get; set; }
        public short? Addon { get; set; }
        public short? JackPot { get; set; }
        public short? TaxaAdm { get; set; }
        public short? Jantar { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }
        public SituacaoVenda Situacao { get; set; }
        public string NomeTorneio { get; set; }
        public string NomeCliente { get; set; }
        public double ValorTotal { get; set; }
        public long? IdComprovantePagamento { get; set; }
        public virtual Pagamento Pagamento { get; set; }
        public List<ItemTorneio> ItensTorneio { get; set; }
        public double? ValorPago { get; set; }
        public bool Finalizar { get; set; }

        public void AtualizaDados(TorneioCliente torneioCliente)
        {
            this.JackPot = torneioCliente.JackPot;
            this.Jantar = torneioCliente.Jantar;
            this.ReBuy = torneioCliente.ReBuy;
            this.TaxaAdm = torneioCliente.TaxaAdm;
            this.ValorTotal = torneioCliente.ValorTotal;
            this.DataAlteracao = DateTime.Now;
            this.BuyIn = torneioCliente.BuyIn;
            this.BonusBeneficente = torneioCliente.BonusBeneficente;
            this.Addon = torneioCliente.Addon;
            this.ValorPago = torneioCliente.ValorPago;
        }

        public TorneioCliente()
        {
        }
    }
}
