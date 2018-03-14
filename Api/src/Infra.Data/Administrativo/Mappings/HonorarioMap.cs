using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class HonorarioMap: BaseMap<Honorario>
    {
        public HonorarioMap()
        {
            ToTable("Honorario");

            HasKey(p => new {p.SindicanteId, p.ServicoSeguradoraId, p.EventoTipoId, p.ServicoSindicanteId});

            HasRequired(p => p.ServicoSeguradora)
                .WithMany()
                .HasForeignKey(fk => fk.ServicoSeguradoraId);

            HasRequired(p => p.EventoTipo)
                .WithMany()
                .HasForeignKey(fk => fk.EventoTipoId);
            HasRequired(p => p.ServicoSindicante)
                .WithMany()
                .HasForeignKey(fk => fk.ServicoSindicanteId);

            Property(p => p.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal")
                .HasPrecision(5,2)
                .IsRequired();
        }
    }
}
