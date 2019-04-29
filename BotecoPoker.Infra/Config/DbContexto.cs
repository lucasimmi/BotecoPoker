using BotecoPoker.Dominio.Entidades;
using BotecoPoker.Dominio.InterfacesRepositorio;
using BotecoPoker.Infra.Mapeamentos;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace BotecoPoker.Infra.Config
{
    public class DbContexto : DbContext
    {
        public DbContexto() : 
            base("Default")
        {
            Database.SetInitializer<DbContexto>(null);
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Configurations.Add(new ProdutoMapeamento());
            dbModelBuilder.Configurations.Add(new ItemVendaMapeamento());
            dbModelBuilder.Configurations.Add(new ClienteMapeamento());
            dbModelBuilder.Configurations.Add(new UsuarioMapeamento());
            dbModelBuilder.Configurations.Add(new PreVendaMapeamento());
            dbModelBuilder.Configurations.Add(new TorneioMapeamento());
            dbModelBuilder.Configurations.Add(new TorneioClienteMapeamento());
            dbModelBuilder.Configurations.Add(new VendaMapeamento());
            dbModelBuilder.Configurations.Add(new CashGameMapeamento());
            dbModelBuilder.Configurations.Add(new CaixaMapeamento());
            dbModelBuilder.Configurations.Add(new PagamentoMapeamento());
            dbModelBuilder.Configurations.Add(new ParcelamentoPagamentoMapeamento());
            dbModelBuilder.HasDefaultSchema("dbo");
        }

        public int Salvar()
        {
            try
            {
                var linhasAfetadas = SaveChanges();
                return linhasAfetadas;
            }
            catch
            {
                return 0;
            }
           
        }

        public void FecharConexao()
        {
            Database.Connection.Close();
            Dispose();
        }
    }
}
