using BotecoPoker.Dominio.Utils;
using System;

namespace BotecoPoker.Dominio.Entidades
{
    public class Cliente : Entidade<long>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Apelido { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Endereco { get; set; }
        public short? Numero { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public virtual Usuario UsuarioCadastro { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public virtual Usuario UsuarioAlteracao { get; set; }
        public string Codigo { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public double? Saldo { get; set; }

        public string FormatarCPF(string cpf)
        {
            if (cpf == "" || cpf == "" || cpf == "..-")
                return "";
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }
    }
}
