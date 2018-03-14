using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class EnderecoSindicanteMap: BaseMap<EnderecoSindicante>
    {
        public EnderecoSindicanteMap()
        {
            ToTable("EnderecoSindicante");

            HasRequired(p => p.Endereco)
                .WithMany()
                .HasForeignKey(fk => fk.EnderecoId);

            HasRequired(p => p.EnderecoTipo)
               .WithMany()
               .HasForeignKey(fk => fk.EnderecoTipoId);

            Property(p => p.EnderecoTipoId)
               .HasColumnName("EnderecoTipoId")
               .HasColumnType("int")
               .IsRequired();
            Property(p => p.EnderecoId)
               .HasColumnName("EnderecoId")
               .HasColumnType("int")
               .IsRequired();
            Property(p => p.Numero)
                .HasColumnType("Numero")
                .HasColumnType("char")
                .HasMaxLength(6)
                .IsRequired();
            Property(p => p.SindicanteId)
                .HasColumnName("SindicanteId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Complemento)
                .HasColumnType("Complemento")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsOptional();
        }
    }
}
