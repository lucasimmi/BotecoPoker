using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class ItemVendaMapeamento : EntityTypeConfiguration<ItemVenda>
    {
        public ItemVendaMapeamento()
        {
            ToTable("ItemVenda", "dbo");
            HasKey(d => d.Id);
            Property(d => d.QtdProduto).HasColumnType("smallint").IsRequired();
            Property(d => d.ValorProduto).HasColumnType("float").IsRequired();
            Property(d => d.ValorTotal).HasColumnType("float").IsRequired();
            HasRequired(d => d.Produto).WithMany().HasForeignKey(d => d.IdProduto);
            HasRequired(d => d.Venda).WithMany().HasForeignKey(d => d.IdVenda);
        }
    }
}
