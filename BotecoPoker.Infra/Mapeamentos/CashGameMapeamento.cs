using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class CashGameMapeamento : EntityTypeConfiguration<CashGame>
    {
        public CashGameMapeamento()
        {
            ToTable("CashGame");
            HasKey(d => d.Id);
            Property(d => d.Situacao).HasColumnType("smallint").IsRequired();
            HasRequired(d => d.Cliente).WithMany().HasForeignKey(d => d.IdCliente);
            HasRequired(d => d.UsuarioCadastro).WithMany().HasForeignKey(d => d.IdUsuarioCadastro);
            HasOptional(d => d.UsuarioAlteracao).WithMany().HasForeignKey(d => d.IdUsuarioAlteracao);
            Property(d => d.DataAlteracao).HasColumnType("datetime2").IsOptional();
            Property(d => d.DataCadastro).HasColumnType("datetime2").IsRequired();
            Property(d => d.Situacao).HasColumnType("smallint").IsRequired();
            HasOptional(d => d.Pagamento).WithMany().HasForeignKey(d => d.IdComprovantePagamento);
            Ignore(d => d.NomeCliente);
        }
    }
}
