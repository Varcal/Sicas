using Domain.Account.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Account.Mappings
{
    public class PerfilMap: BaseMap<Perfil>
    {
        public PerfilMap()
        {
            ToTable("Perfil");

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
