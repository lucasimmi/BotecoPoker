using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class VendaMapeamento : EntityTypeConfiguration<Venda>
    {
        public VendaMapeamento()
        {
            ToTable("Venda", "dbo");
            HasKey(d => d.Id);
            Property(d => d.QtdItem).HasColumnType("smallint").IsRequired();
            Property(d => d.DataVenda).HasColumnType("datetime2").IsRequired();
            Property(d => d.Situacao).HasColumnType("smallint").IsRequired();
            HasRequired(d => d.Usuario).WithMany().HasForeignKey(d => d.IdUsuario);
            HasRequired(d => d.Cliente).WithMany().HasForeignKey(d => d.IdCliente);
            HasOptional(d => d.Pagamento).WithMany().HasForeignKey(d => d.IdComprovantePagamento);
            Ignore(d => d.NomeCliente);
        }
    }
}
