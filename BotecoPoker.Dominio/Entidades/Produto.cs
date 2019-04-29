using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.Entidades
{
    public class Produto : Entidade<int>
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public double ValorCompra { get; set; }
        public short QtdEstoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }

        public string FormatarValor(double? valor)
        {
            if (valor == null)
                valor = 0;
            return valor.Value.ToString("c2");
        }
    }
}
