using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class ServicoSindicanteMap: BaseMap<ServicoSindicante>
    {
        public ServicoSindicanteMap()
        {
            ToTable("ServicoSindicante");

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
