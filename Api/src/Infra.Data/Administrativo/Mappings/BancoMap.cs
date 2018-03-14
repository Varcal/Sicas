using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class BancoMap: BaseMap<Banco>
    {
        public BancoMap()
        {
            ToTable("Banco");

            Property(p => p.Numero)
                .HasColumnName("Numero")
                .HasColumnType("char")
                .HasMaxLength(3)
                .IsRequired();
            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
