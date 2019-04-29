using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class TorneioMapeamento : EntityTypeConfiguration<Torneio>
    {
        public TorneioMapeamento()
        {
            ToTable("Torneio", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Nome).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(d => d.BuyIn).HasColumnType("float").IsOptional();
            Property(d => d.ReBuy).HasColumnType("float").IsOptional();
            Property(d => d.Addon).HasColumnType("float").IsOptional();
            Property(d => d.JackPot).HasColumnType("float").IsOptional();
            Property(d => d.Jantar).HasColumnType("float").IsOptional();
            Property(d => d.TaxaAdm).HasColumnType("float").IsOptional();
            Property(d => d.DataAlteracao).HasColumnType("datetime2").IsOptional();
            Property(d => d.DataCadastro).HasColumnType("datetime2").IsRequired();
            Property(d => d.Ativo).HasColumnType("smallint").IsRequired();
            HasRequired(d => d.UsuarioCadastro).WithMany().HasForeignKey(d => d.IdUsuarioCadastro);
            HasOptional(d => d.UsuarioAlteracao).WithMany().HasForeignKey(d => d.IdUsuarioAlteracao);
        }
    }
}
