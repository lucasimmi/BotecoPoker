using BotecoPoker.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace BotecoPoker.Infra.Mapeamentos
{
    public class ParcelamentoPagamentoMapeamento : EntityTypeConfiguration<ParcelamentoPagamento>
    {
        public ParcelamentoPagamentoMapeamento()
        {
            ToTable("ParcelamentoPagamento", "dbo");
            HasKey(d => d.Id);
            Property(d => d.TipoFinalizador).HasColumnType("smallint").IsRequired();
            Property(d => d.ValorPago).HasColumnType("float").IsRequired();
            Property(d => d.DataPagamento).HasColumnType("datetime").IsRequired();
            HasOptional(d => d.Pagamento).WithMany().HasForeignKey(d => d.IdPagamento);
            Ignore(d => d.IdCliente);
            Ignore(d => d.Saldo);
            Ignore(d => d.TrocoSaldo);
        }
    }
}
