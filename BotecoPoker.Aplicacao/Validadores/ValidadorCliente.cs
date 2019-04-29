using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Validadores
{
    public class ValidadorCliente
    {
        private IClienteRepositorio ClienteRepositorio;

        public string Validar(Cliente cliente, IClienteRepositorio clienteRepositorio)
        {
            ClienteRepositorio = clienteRepositorio;
            StringBuilder erros = new StringBuilder();

            if (!string.IsNullOrEmpty(cliente.Codigo))
            {
                if (ExisteCodigo(cliente.Codigo, cliente.Id))
                    erros.Append("Código não pode repetir! ");
            }

            if (!string.IsNullOrEmpty(cliente.CPF))
            {
                if (cliente.CPF.Length != 14)
                    erros.AppendLine("Favor digitar um CPF válido! ");
                else if (ExistCpf(cliente.CPF, cliente.Id))
                    erros.AppendLine("CPF já cadastrado no banco de dados! ");
                else
                {
                    if (!CPFValido(cliente.CPF))
                        erros.AppendLine("Favor informar um CPF válido! ");
                }
            }

            if (string.IsNullOrEmpty(cliente.Nome))
                erros.AppendLine("Nome deve ser informado! ");
            else if (cliente.Nome.Length > 50 || cliente.Nome.Length < 10)
                erros.AppendLine("Favor informar um nome válido! ");

            if (cliente.Telefone.TemValor())
            {
                if (cliente.Telefone.Length != 14)
                    erros.AppendLine("Telefone inválido! ");
            }
            if (!string.IsNullOrWhiteSpace(cliente.Celular))
            {
                if (cliente.Celular.Length != 15)
                    erros.AppendLine("Celular inválido! ");
            }
            if (cliente.Agencia.TemValor())
            {
                if (cliente.Agencia.Length > 5)
                    erros.AppendLine("Agência pode conter no maximo 5 caracteres! ");
            }

            if (cliente.Conta.TemValor())
            {
                if (cliente.Conta.Length > 20)
                    erros.AppendLine("Conta pode conter no maximo 20 caracteres!! ");
            }

            if (cliente.RG.TemValor())
            {
                if (cliente.RG.Length > 20 || cliente.RG.Length < 5)
                    erros.AppendLine("RG inválido! ");
            }

            if (cliente.Numero != null)
            {
                if (cliente.Numero > 99999 || cliente.Numero <= 0)
                    erros.AppendLine("Numero inválido! ");
            }

            if (cliente.Apelido.TemValor())
            {
                if (cliente.Apelido.Length > 30)
                    erros.AppendLine("Apelido pode conter no maximo 30 caracteres! ");
            }

            if (cliente.Complemento.TemValor())
            {
                if (cliente.Complemento.Length > 40)
                    erros.AppendLine("Complemento pode conter no maximo 40 caracteres! ");
            }

            if (cliente.Endereco.TemValor())
            {
                if (cliente.Endereco.Length > 40)
                    erros.AppendLine("Endereço pode conter no maximo 40 caracteres! ");
            }

            if (cliente.Email.TemValor())
            {
                Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
                if (!rg.IsMatch(cliente.Email))
                    erros.AppendLine("Favor informar um email válido");
            }
            return erros.ToString();
        }

        public bool ExisteCodigo(string codigo, long id)
        {
            return ClienteRepositorio.Filtrar(d => d.Codigo == codigo && d.Id != id).Any();
        }

        public bool ExistCpf(string cpf, long id)
        {
            return ClienteRepositorio.Filtrar(d => d.CPF == cpf && d.Id != id).Any();
        }

        public static bool CPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
