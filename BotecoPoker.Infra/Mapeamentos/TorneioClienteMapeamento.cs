using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class TorneioClienteMapeamento : EntityTypeConfiguration<TorneioCliente>
    {
        public TorneioClienteMapeamento()
        {
            ToTable("TorneioCliente", "dbo");
            HasKey(d => d.Id);
            Property(d => d.BuyIn).HasColumnType("smallint").IsOptional();
            Property(d => d.ReBuy).HasColumnType("smallint").IsOptional();
            Property(d => d.Addon).HasColumnType("smallint").IsOptional();
            Property(d => d.JackPot).HasColumnType("smallint").IsOptional();
            Property(d => d.Jantar).HasColumnType("smallint").IsOptional();
            Property(d => d.TaxaAdm).HasColumnType("smallint").IsOptional();
            Property(d => d.BonusBeneficente).HasColumnType("varchar").HasMaxLength(30).IsOptional();
            Property(d => d.DataAlteracao).HasColumnType("datetime2").IsOptional();
            Property(d => d.DataCadastro).HasColumnType("datetime2").IsRequired();
            Property(d => d.Situacao).HasColumnType("smallint").IsRequired();
            Property(d => d.ValorPago).HasColumnType("float").IsRequired();
            HasRequired(d => d.UsuarioCadastro).WithMany().HasForeignKey(d => d.IdUsuarioCadastro);
            HasOptional(d => d.UsuarioAlteracao).WithMany().HasForeignKey(d => d.IdUsuarioAlteracao);
            HasRequired(d => d.Torneio).WithMany().HasForeignKey(d => d.IdTorneio);
            HasRequired(d => d.Cliente).WithMany().HasForeignKey(d => d.IdCliente);
            HasOptional(d => d.Pagamento).WithMany().HasForeignKey(d => d.IdComprovantePagamento);
            Ignore(d => d.ItensTorneio);
            Ignore(d => d.NomeTorneio);
            Ignore(d => d.NomeCliente);
            Ignore(d => d.ValorTotal);
            Ignore(d => d.Finalizar);
        }
    }
}
