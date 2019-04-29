using BotecoPoker.Aplicacao.Validadores;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using BotecoPoker.Infra.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotecoPoker.Aplicacao.Servicos
{
    public class ClienteAplicacao
    {
        [Inject]
        public IClienteRepositorio ClienteRepositorio { get; set; }
        [Inject]
        public ValidadorCliente Validador { get; set; }
        [Inject]
        public IDbContexto Contexto { get; set; }
        [Inject]
        public AutenticacaoAplicacao AutenticacaoAplicacao { get; set; }
        [Inject]
        public IUsuarioRepositorio UsuarioRepositorio { get; set; }

        public string CadastrarCliente(Cliente Cliente)
        {
            var result = Validador.Validar(Cliente, ClienteRepositorio);
            if (result != "" && result != null)
                return result;
            Cliente.DataCadastro = DateTime.Now;
            Cliente.IdUsuarioCadastro = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            ClienteRepositorio.Cadastrar(Cliente);
            var teste = Contexto.Salvar();
            return result;
        }

        public string AtualizarCliente(Cliente cliente)
        {
            var result = Validador.Validar(cliente, ClienteRepositorio);
            if (result != "")
                return result;
            cliente.IdUsuarioAlteracao = AutenticacaoAplicacao.ObterUsuarioLogado().Id;
            cliente.DataAlteracao = DateTime.Now;
            ClienteRepositorio.Atualizar(cliente);
            var test = Contexto.Salvar();
            return result;
        }

        public int ExcluirCliente(Cliente Cliente)
        {
            ClienteRepositorio.Excluir(Cliente);
            return Contexto.Salvar();
        }

        public Cliente BuscarClientePorId(long Id)
        {
            return ClienteRepositorio.Buscar(Id);
        }
        public PaginacaoModel<Cliente, FiltroCliente>
            Filtrar(PaginacaoModel<Cliente, FiltroCliente> paginacao)
        {
            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroCliente(paginacao.Parametro1, paginacao.Parametro12, paginacao.Parametro13);
            else
            {
                paginacao.Parametro1 = paginacao?.Filtro?.Nome;
                paginacao.Parametro12 = paginacao?.Filtro?.Apelido;
                paginacao.Parametro13 = paginacao?.Filtro?.Codigo;
            }

            var query = ClienteRepositorio.Query();

            if (paginacao.Filtro.Apelido.TemValor())
            {
                query = query.Where(d => d.Apelido.Contains(paginacao.Filtro.Apelido));
            }
            if (paginacao.Filtro.Codigo.TemValor())
            {
                query = query.Where(d => d.Codigo.Contains(paginacao.Filtro.Codigo));
            }
            if (paginacao.Filtro.Nome.TemValor())
            {
                query = query.Where(d => d.Nome.Contains(paginacao.Filtro.Nome));
            }

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            return paginacao;
        }
    }
}
