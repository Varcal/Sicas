using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class SindicanteTipoMap: BaseMap<SindicanteTipo>
    {
        public SindicanteTipoMap()
        {
            ToTable("SindicanteTipo");

            Property(p => p.Nome)
                .HasColumnName("SindicanteTipo")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
