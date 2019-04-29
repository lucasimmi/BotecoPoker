using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class ProdutoMapeamento : EntityTypeConfiguration<Produto>
    {
        public ProdutoMapeamento()
        {
            ToTable("Produto", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Nome).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            Property(d => d.Valor).HasColumnType("float").IsRequired();
            Property(d => d.ValorCompra).HasColumnType("float").IsRequired();
            Property(d => d.QtdEstoque).HasColumnType("smallint").IsRequired();
            Property(d => d.DataCadastro).HasColumnType("datetime2").IsRequired();
            Property(d => d.DataAlteracao).HasColumnType("datetime2").IsOptional();
            HasRequired(d => d.UsuarioCadastro).WithMany().HasForeignKey(d => d.IdUsuarioCadastro);
            HasOptional(d => d.UsuarioAlteracao).WithMany().HasForeignKey(d => d.IdUsuarioAlteracao);
        }
    }
}
