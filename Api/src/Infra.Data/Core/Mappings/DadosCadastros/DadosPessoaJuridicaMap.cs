using Domain.Core.Entities.DadosCadastros;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.DadosCadastros
{
    public class DadosPessoaJuridicaMap: BaseMap<DadosPessoaJuridica>
    {
        public DadosPessoaJuridicaMap()
        {
            ToTable("DadosPessoaJuridica");

            Property(p => p.RazaoSocial)
                .HasColumnName("RazaoSocial")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();
            Property(p => p.NomeFantasia)
                .HasColumnName("NomeFantasia")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsOptional();
            Property(p => p.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("char")
                .HasMaxLength(14)
                .HasUniqueIndexAnnotation("IX_Cnpj_Unique")
                .IsRequired();
            Property(p => p.InscEst)
                .HasColumnName("InscEst")
                .HasColumnType("char")
                .HasMaxLength(20)
                .IsOptional();
        }
    }
}
