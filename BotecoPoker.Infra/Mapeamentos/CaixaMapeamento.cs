using BotecoPoker.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class CaixaMapeamento : EntityTypeConfiguration<Caixa>
    {
        public CaixaMapeamento()
        {
            ToTable("Caixa", "dbo");
            HasKey(d => d.Id);
            Property(d => d.DataAbertura).HasColumnType("datetime").IsRequired();
            Property(d => d.DataFechamento).HasColumnType("datetime").IsOptional();
            Property(d => d.IdUsuarioAbertura).HasColumnType("int").IsRequired();
            Property(d => d.IdUsuarioFechamento).HasColumnType("int").IsOptional();
            Property(d => d.Ativo).HasColumnType("smallint").IsRequired();
            Ignore(d => d.ValorCashGame);
            Ignore(d => d.ValorTorneios);
            Ignore(d => d.ValorVenda);
        }
    }
}
