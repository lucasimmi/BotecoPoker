using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class PagamentoMapeamento : EntityTypeConfiguration<Pagamento>
    {
        public PagamentoMapeamento()
        {
            ToTable("Pagamento", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Data).HasColumnType("datetime").IsRequired();
            Property(d => d.Situacao).HasColumnType("smallint").IsRequired();
            Property(d => d.ValorAberto).HasColumnType("float").IsRequired();
            Property(d => d.ValorTotal).HasColumnType("float").IsRequired();
            HasRequired(d => d.Cliente).WithMany().HasForeignKey(d => d.IdCliente);
        }
    }
}
