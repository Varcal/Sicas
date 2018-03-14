using Domain.Core.Entities.Enderecos;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.Enderecos
{
    public class EnderecoMap: BaseMap<Endereco>
    {
        public EnderecoMap()
        {
            ToTable("Endereco");

            HasRequired(p => p.Cidade)
                .WithMany()
                .HasForeignKey(fk => fk.CidadeId);
                
            Property(p => p.Logradouro)
                .HasColumnName("Logradouro")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();
            Property(p => p.LogradouroFonetizado)
                .HasColumnName("LogradouroFonetizado")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();
            Property(p => p.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsOptional();
            Property(p => p.Cep)
                .HasColumnName("Cep")
                .HasColumnType("char")
                .HasMaxLength(8)
                .IsRequired();
            Property(p => p.CidadeId)
                .HasColumnName("CidadeId")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
