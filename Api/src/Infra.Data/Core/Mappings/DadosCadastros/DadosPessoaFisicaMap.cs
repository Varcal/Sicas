using Domain.Core.Entities.DadosCadastros;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Core.Mappings.DadosCadastros
{
    public class DadosPessoaFisicaMap : BaseMap<DadosPessoaFisica>
    {
        public DadosPessoaFisicaMap()
        {
            ToTable("DadosPessoaFisica");

            Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
            Property(p => p.Cpf)
                .HasColumnName("Cpf")
                .HasColumnType("char")
                .HasMaxLength(11)
                .HasUniqueIndexAnnotation("IX_Cpf_Unique")
                .IsRequired();
            Property(p => p.Rg)
                .HasColumnName("Rg")
                .HasColumnType("char")
                .HasMaxLength(20)
                .IsOptional();
        }
    }
}
