using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.Enderecos
{
    public class EstadoMap: BaseMap<Estado>
    {
        public EstadoMap()
        {
            ToTable("Estado");

            HasRequired(p => p.Pais)
                .WithMany()
                .HasForeignKey(p => p.PaisId);

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
            Property(p => p.Uf)
                .HasColumnName("Uf")
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}
