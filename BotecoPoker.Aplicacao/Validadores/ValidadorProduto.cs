using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Aplicacao.Validadores
{
    public class ValidadorProduto
    {
        [Inject]
        public IProdutoRepositorio ProdutoRepositorio { get; set; }

        public string Validar(Produto Produto)
        {
            StringBuilder erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Produto.Nome))
                erros.AppendLine("Favor informar o nome do produto!");
            if (Produto.Nome.Length < 2 || Produto.Nome.Length > 30)
                erros.AppendLine("Nome do produto conter no mínimo 2 e no máximo 30 caracteres!");
            else
                if (ExistProduto(Produto.Nome, Produto.Id))
                erros.AppendLine("Produto já cadastrado!");
            if (Produto.Valor <= 0)
                erros.AppendLine("Favor informar valor do produto!");
            if (Produto.Valor >= 999)
                erros.AppendLine("Valor de venda não permitido!");

            if (Produto.ValorCompra <= 0)
                erros.AppendLine("Favor informar valor de compra do produto!");
            if (Produto.ValorCompra >= 999)
                erros.AppendLine("Valor de compra não permitido!");

            if (Produto.QtdEstoque >= 1001)
                erros.AppendLine("Quantidade de estoque não permitida!");

            return erros.ToString();
        }
        public bool ExistProduto(string nome, int? id)
        {
            if (id > 0)
                return ProdutoRepositorio.Filtrar(d => d.Nome == nome && d.Id != id).Any();
            return ProdutoRepositorio.Filtrar(d => d.Nome == nome).Any();
        }
    }
}
