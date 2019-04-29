using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class ProdutoAplicacao
    {
        [Inject]
        public IProdutoRepositorio ProdutoRepositorio { get; set; }

        [Inject]
        public ValidadorProduto Validador { get; set; }

        [Inject]
        public IDbContexto Contexto { get; set; }

        public IEnumerable<SelectListItem> ComboProduto()
        {
            return ProdutoRepositorio.ObterComboProdutos();
        }

        public string CadastroProduto(Produto entidade)
        {
            var result = Validador.Validar(entidade);
            if (result.TemValor())
                return result;
            entidade.DataCadastro = DateTime.Now;
            entidade.IdUsuarioCadastro = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            ProdutoRepositorio.Cadastrar(entidade);
            Contexto.Salvar();
            return result;
        }

        public string AlterarProduto(Produto entidade)
        {
            var result = Validador.Validar(entidade);
            if (result.TemValor())
                return result;
            entidade.DataAlteracao = DateTime.Now;
            entidade.IdUsuarioAlteracao = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            ProdutoRepositorio.Atualizar(entidade);
            Contexto.Salvar();
            return result;
        }

        public Produto BuscarPorId(int id)
        {
            return ProdutoRepositorio.Buscar(id);
        }

        public void ExcluirProduto(Produto entidade)
        {
             ProdutoRepositorio.Excluir(entidade);
        }

        public PaginacaoModel<Produto, FiltroProduto> Filtrar(PaginacaoModel<Produto, FiltroProduto> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroProduto(paginacao.Parametro1);
            else
                paginacao.Parametro1 = paginacao.Filtro.Nome;
            var query = ProdutoRepositorio.Query();
            if (paginacao.Filtro.Nome.TemValor())
                query = query.Where(d => d.Nome.Contains(paginacao.Filtro.Nome));

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            Contexto.FecharConexao();
            return paginacao;
        }

        internal double ObterValorProduto(int idProduto)
        {
            return ProdutoRepositorio.Filtrar(d => d.Id == idProduto).Select(d => d.Valor).FirstOrDefault();
        }
    }
}
