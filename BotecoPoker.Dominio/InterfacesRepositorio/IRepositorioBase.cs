using BotecoPoker.Dominio.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BotecoPoker.Dominio.InterfacesRepositorio
{
    public interface IRepositorioBase<Ent, Id> where Ent : Entidade<Id>
    {
        void Cadastrar(Ent entidade);
        void Atualizar(Ent entidade);
        void Excluir(Ent entidade);
        Ent Buscar(Id id);
        IQueryable<Ent> Filtrar(Expression<Func<Ent, bool>> predicate);
        IQueryable<Ent> Query();
    }
}
