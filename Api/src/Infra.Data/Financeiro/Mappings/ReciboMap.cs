using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class ReciboMap: BaseMap<Recibo>
    {
        public ReciboMap()
        {
            ToTable("Recibo");

            HasMany(x => x.Lancamentos)
                .WithRequired()
                .HasForeignKey(fk => fk.ReciboId);

            HasRequired(p => p.Sindicante)
                .WithMany()
                .HasForeignKey(fk => fk.SindicanteId);
            HasRequired(p => p.Processo)
                .WithMany()
                .HasForeignKey(fk => fk.ProcessoId);

            Property(p => p.ProcessoId)
                .HasColumnName("ProcessoId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.SindicanteId)
                .HasColumnName("SindicanteId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Status)
                .HasColumnName("Status")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
