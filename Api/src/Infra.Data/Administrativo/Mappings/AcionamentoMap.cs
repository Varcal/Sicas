using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class AcionamentoMap: BaseMap<Acionamento>
    {
        public AcionamentoMap()
        {
            ToTable("Acionamento");

            Property(x => x.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();
            Property(x => x.Arquivo)
                .HasColumnName("Arquivo")
                .HasColumnType("varbinary(MAX)")
                .IsRequired();

        }
    }
}
