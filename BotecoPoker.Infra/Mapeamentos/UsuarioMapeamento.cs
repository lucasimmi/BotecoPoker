using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class UsuarioMapeamento : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapeamento()
        {
            ToTable("Usuario", "dbo");
            HasKey(d => d.Id);
            Property(d => d.Login).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            Property(d => d.Nome).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(d => d.Senha).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            Ignore(d => d.ConfirmaSenha);
            Ignore(d => d.ConfimaNovaSenha);
            Ignore(d => d.NovaSenha);
        }
    }
}
