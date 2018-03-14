using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class DespesaSeguradoraMap: BaseMap<DespesaSeguradora>
    {
        public DespesaSeguradoraMap()
        {
            ToTable("DespesaSeguradora");

            HasRequired(p=>p.Processo)
                .WithMany()
                .HasForeignKey(fk=>fk.ProcessoId);

            HasMany(p => p.DespesasSeguradoraItems)
                .WithRequired()
                .HasForeignKey(fk => fk.DespesaSeguradoraId)
                .WillCascadeOnDelete(true);

            Property(p => p.Id).HasColumnName("NumeroRecibo");

            Property(p => p.Cia).HasMaxLength(200).IsRequired();
            Property(p => p.Sinistro).HasMaxLength(50).IsRequired();
            Property(p => p.Segurado).HasMaxLength(200).IsRequired();
            Property(p => p.Analista).HasMaxLength(200).IsRequired();
            Property(p => p.DataConclusao).HasColumnType("date").IsRequired();
            Property(p => p.ValorKm).HasPrecision(18,2).IsRequired();
        }
    }
}
