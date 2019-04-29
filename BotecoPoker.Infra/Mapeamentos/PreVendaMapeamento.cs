using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class PreVendaMapeamento : EntityTypeConfiguration<PreVenda>
    {
        public PreVendaMapeamento()
        {
            ToTable("PreVenda", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Ativo).HasColumnType("smallint").IsRequired();
            Property(d => d.Quantidade).HasColumnType("smallint").IsRequired();
            Property(d => d.DataHora).HasColumnType("datetime2").IsRequired();
            HasRequired(d => d.Produto).WithMany().HasForeignKey(d => d.IdProduto);
            HasOptional(d => d.Venda).WithMany().HasForeignKey(d => d.IdVenda);
            HasRequired(d => d.Usuario).WithMany().HasForeignKey(d => d.IdUsuario);

            Ignore(d => d.NomeProduto);
        }
    }
}
