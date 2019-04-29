using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using BotecoPoker.Dominio.Utils;
using BotecoPoker.Infra.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class ClienteRepositorio : RepositorioBase<Cliente, long>, IClienteRepositorio
    {
        public ClienteRepositorio(DbContexto db) : base(db)
        {
        }

        public PaginacaoModel<Cliente, FiltroPagamento> ObterClientesComPendencias(PaginacaoModel<Cliente, FiltroPagamento> paginacao)
        {
            var ids = Db.Set<Venda>().Where(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).Select(d => d.IdCliente).ToList();
            ids.AddRange(Db.Set<CashGame>().Where(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).Select(d => d.IdCliente).ToList());
            ids.AddRange(Db.Set<TorneioCliente>().Where(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).Select(d => d.IdCliente).ToList());
            ids.AddRange(Db.Set<Pagamento>().Where(d => d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente).Select(d => d.IdCliente).ToList());
            ids.Distinct();

            var query = Set.Where(d => ids.Contains(d.Id));

            if (paginacao.Filtro == null)
                paginacao.Filtro = new FiltroPagamento(paginacao.Parametro1, paginacao.Parametro12, paginacao.Parametro13);
            else
            {
                paginacao.Parametro1 = paginacao?.Filtro?.NomeCliente;
                paginacao.Parametro12 = paginacao?.Filtro?.ApelidoCliente;
                paginacao.Parametro13 = paginacao?.Filtro?.CodigoCliente;
            }
            if (paginacao.Filtro.ApelidoCliente.TemValor())
                query = query.Where(d => d.Apelido.Contains(paginacao.Filtro.ApelidoCliente));
            if (paginacao.Filtro.NomeCliente.TemValor())
                query = query.Where(d => d.Nome.Contains(paginacao.Filtro.NomeCliente));
            if (paginacao.Filtro.CodigoCliente.TemValor())
                query = query.Where(d => d.Codigo.Contains(paginacao.Filtro.CodigoCliente));

            paginacao.ListaModel = query.OrderBy(d => d.Id).Skip(((paginacao.Pagina - 1) * 10)).Take(10).ToList();
            paginacao.QtdPaginas = query.Count().CalculaQtdPaginas().TransformaEmLista();
            return paginacao;
        }

        public Cliente ObterPorCodigo(string codigo) => Set.FirstOrDefault(d => d.Codigo == codigo);
    }
}
