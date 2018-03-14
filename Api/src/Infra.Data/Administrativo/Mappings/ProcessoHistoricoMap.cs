using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class ProcessoHistoricoMap: BaseMap<ProcessoHistorico>
    {
        public ProcessoHistoricoMap()
        {
            ToTable("ProcessoHistorico");

            HasKey(p => p.Id);

            HasRequired(p => p.Seguradora)
                .WithMany()
                .HasForeignKey(fk => fk.SeguradoraId);
            
            HasRequired(p => p.Sindicante)
                .WithMany()
                .HasForeignKey(fk => fk.SindicanteId);

            HasRequired(p => p.Status)
                .WithMany()
                .HasForeignKey(fk => fk.StatusId);

            Property(p => p.IniciadoEm)
                .HasColumnName("IniciadoEm")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsRequired();
            Property(p => p.FinalizadoEm)
                .HasColumnName("FinalizadoEm")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsOptional();
        }
    }
}
