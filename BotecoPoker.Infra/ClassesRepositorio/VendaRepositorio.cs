using System.Collections.Generic;
using System.Linq;
using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.modelos;
using BotecoPoker.Infra.Config;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class VendaRepositorio : RepositorioBase<Venda, long>, IVendaRepositorio
    {
        public VendaRepositorio(DbContexto db) : base(db)
        {
        }

        public List<VendaModel> ObterVendaModelPendente(long idCliente)
        {
            return Set.Where(d => d.IdCliente == idCliente && d.Situacao == Dominio.Enumeradores.SituacaoVenda.Pendente)
                .Select(d => new VendaModel
                {
                    DataVenda = d.DataVenda,
                    IdVenda = d.Id,
                    QtdItem = d.QtdItem,
                    Valor = d.Valor,
                    PreVendas = Db.Set<PreVenda>().Where(p => p.IdVenda == d.Id).ToList()
                }).ToList();
        }
    }
}
