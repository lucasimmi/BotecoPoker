using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Dominio.Utils;
using BotecoPoker.Infra.ClassesRepositorio;
using BotecoPoker.Infra.Config;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BotecoPoker.Infra.ClassesRepositorio
{
    public class RepositorioBase<Ent, Id> : IRepositorioBase<Ent, Id> where Ent : Entidade<Id> 
    {
        public DbContexto Db;
        public DbSet<Ent> Set;

        public RepositorioBase(DbContexto db)
        {
            Db = db;
            Set = Db.Set<Ent>();
        }

        public void Atualizar(Ent entidade)
        {
            var original = Set.Find(entidade.Id);
            Db.Entry(original).State = EntityState.Modified;
            Db.Entry(original).OriginalValues.SetValues(entidade);
            Db.Entry(original).CurrentValues.SetValues(entidade);
        }
        public Ent Buscar(Id id) => Set.Find(id);

        public void Cadastrar(Ent entidade) => Set.Add(entidade);

        public void Excluir(Ent entidade) => Set.Remove(entidade);

        public IQueryable<Ent> Filtrar(Expression<Func<Ent, bool>> predicate) => Db.Set<Ent>().Where(predicate);

        public IQueryable<Ent> Query() => Set;
    }
}