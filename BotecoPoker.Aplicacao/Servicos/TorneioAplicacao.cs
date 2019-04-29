using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.Enumeradores;
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
    public class TorneioAplicacao
    {
        [Inject]
        public ITorneioRepositorio TorneioRepositorio { get; set; }
        [Inject]
        public ValidadorTorneio Validador { get; set; }
        [Inject]
        public IDbContexto Contexto { get; set; }
        [Inject]
        public AutenticacaoAplicacao AutenticacaoAplicacao { get; set; }

        public string CadastrarTorneio(Torneio entidade)
        {
            var result = Validador.ValidaTorneio(entidade);
            if (result != "")
                return result;
            entidade.DataCadastro = DateTime.Now;
            entidade.IdUsuarioCadastro = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            TorneioRepositorio.Cadastrar(entidade);
            Contexto.Salvar();
            return result;
        }

        public string AlterarTorneio(Torneio entidade)
        {
            var result = Validador.ValidaTorneio(entidade);
            if (result != "")
                return result;
            entidade.DataAlteracao = DateTime.Now;
            entidade.IdUsuarioAlteracao = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            TorneioRepositorio.Atualizar(entidade);
            Contexto.Salvar();
            return result;
        }

        public Torneio BuscarPorId(int id)
        {
            return TorneioRepositorio.Buscar(id);
        }

        public void ExcluirTorneio(int id)
        {
            TorneioRepositorio.Excluir(TorneioRepositorio.Buscar(id));
        }

        public PaginacaoModel<Torneio, FiltroTorneio> Filtrar(PaginacaoModel<Torneio, FiltroTorneio> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroTorneio(paginacao.Parametro1);
            else
            {
                paginacao.Parametro1 = paginacao.Filtro.Nome;
            }
            var query = TorneioRepositorio.Query();
            if (paginacao.Filtro.Nome.TemValor())
                query = query.Where(d => d.Nome.Contains(paginacao.Filtro.Nome));
            if (paginacao.Filtro.Ativo == Ativo.Ativo || paginacao.Filtro.Ativo == Ativo.Ativo)
                query = query.Where(d => d.Ativo == paginacao.Filtro.Ativo);

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            return paginacao;
        }

        public IEnumerable<SelectListItem> ObterComboTorneio()
        {
            return TorneioRepositorio.ObterComboTorneio();
        }
    }
}
