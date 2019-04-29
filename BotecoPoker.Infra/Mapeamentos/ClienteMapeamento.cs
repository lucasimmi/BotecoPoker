using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class ClienteMapeamento :EntityTypeConfiguration<Cliente>
    {
        public ClienteMapeamento()
        {
            ToTable("Cliente", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Nome).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(d => d.CPF).HasColumnType("varchar").HasMaxLength(14).IsOptional();
            Property(d => d.RG).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(d => d.Apelido).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(d => d.Telefone).HasColumnType("varchar").HasMaxLength(14).IsOptional();
            Property(d => d.Celular).HasColumnType("varchar").HasMaxLength(15).IsOptional();
            Property(d => d.Endereco).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(d => d.Numero).HasColumnType("smallint").IsOptional();
            Property(d => d.Agencia).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(d => d.Conta).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(d => d.DataCadastro).HasColumnType("datetime2").IsRequired();
            Property(d => d.DataAlteracao).HasColumnType("datetime2").IsOptional();
            Property(d => d.Codigo).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(d => d.Complemento).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(d => d.Saldo).HasColumnName("Saldo").HasColumnType("float").IsOptional();
            Property(d => d.Email).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            HasRequired(d => d.UsuarioCadastro).WithMany().HasForeignKey(d => d.IdUsuarioCadastro);
            HasOptional(d => d.UsuarioAlteracao).WithMany().HasForeignKey(d => d.IdUsuarioAlteracao);
        }
    }
}
