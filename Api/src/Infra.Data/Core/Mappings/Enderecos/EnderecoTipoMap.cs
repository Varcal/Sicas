using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.Enderecos
{
    public class EnderecoTipoMap: BaseMap<EnderecoTipo>
    {
        public EnderecoTipoMap()
        {
            ToTable("EnderecoTipo");

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
