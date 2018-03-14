using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.Enderecos
{
    public class PaisMap: BaseMap<Pais>
    {
        public PaisMap()
        {
            ToTable("Pais");

            
            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
            Property(p => p.Sigla)
                .HasColumnName("Sigla")
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}
