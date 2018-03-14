using Domain.Administrativo.Entities;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class SindicanteInternoMap: BaseMap<SindicanteInterno>
    {
        public SindicanteInternoMap()
        {
            ToTable("SindicanteInterno");

            HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(fk => fk.UsuarioId);

            Property(p => p.UsuarioId)
                .HasColumnName("UsuarioId")
                .HasColumnType("int")
                .HasUniqueIndexAnnotation("IX_UsuarioId")
                .IsRequired();
        }
    }
}
