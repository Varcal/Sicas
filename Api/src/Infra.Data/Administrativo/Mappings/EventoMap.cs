using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class EventoMap:BaseMap<Evento>
    {
        public EventoMap()
        {
            ToTable("Evento");

            #region Relacionamentos

            
            HasRequired(p => p.ServicoSeguradora)
                .WithMany()
                .HasForeignKey(fk => fk.ServicoSeguradoraId);

            HasRequired(p => p.EventoTipo)
                .WithMany()
                .HasForeignKey(fk => fk.EventoTipoId);

            #endregion

            HasKey(p => new {p.SeguradoraId, p.ServicoSeguradoraId, p.EventoTipoId});
        }
    }
}
