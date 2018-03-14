using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.Enderecos
{
    public class CidadeMap: BaseMap<Cidade>
    {
        public CidadeMap()
        {
            ToTable("Cidade");

            HasRequired(p => p.Estado)
                .WithMany()
                .HasForeignKey(p => p.EstadoId);
           

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            Property(p => p.EstadoId)
                .HasColumnName("EstadoId")
                .HasColumnType("int")
                .IsRequired();
           
        }
    }
}
