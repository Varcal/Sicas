using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class SeguradoMap: BaseMap<Segurado>
    {
        public SeguradoMap()
        {
            ToTable("Segurado");

            HasKey(p => p.Id);

            HasOptional(p => p.EnderecoSegurado)
                .WithMany()
                .HasForeignKey(fk => fk.EnderecoSeguradoId);

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
