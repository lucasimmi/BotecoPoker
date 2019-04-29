using BotecoPoker.Dominio.Enumeradores;
using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.Entidades
{
    public class Torneio : Entidade<int>
    {
        public string Nome { get; set; }
        public double? BuyIn { get; set; }
        public double? ReBuy { get; set; }
        public double? Addon { get; set; }
        public double? Jantar { get; set; }
        public double? JackPot { get; set; }
        public double? TaxaAdm { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }
        public Ativo Ativo { get; set; }

        public string FormatarValor(double? val)
        {
            var valor = val ?? 0;
            return valor.ToString("c2");
        }
    }
}
