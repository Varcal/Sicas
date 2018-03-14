using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class ServicoSeguradoraMap : BaseMap<ServicoSeguradora>
    {
        public ServicoSeguradoraMap()
        {
            ToTable("ServicoSeguradora");

            HasMany(p => p.EventoTipos)
                .WithMany(p => p.ServicosSeguradoras)
                .Map(m =>
                {
                    m.ToTable("ServicoSeguradoraEventoTipo");
                    m.MapLeftKey("ServicoSeguradoraId");
                    m.MapRightKey("EventoTipoId");
                });                

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
