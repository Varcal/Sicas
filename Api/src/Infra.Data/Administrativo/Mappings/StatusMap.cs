using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class StatusMap: BaseMap<Status>
    {
        public StatusMap()
        {
            ToTable("Status");

            Property(p => p.Nome)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
