using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class EnderecoDespesaMap: BaseMap<EnderecoDespesa>
    {
        public EnderecoDespesaMap()
        {
            ToTable("EnderecoDespesa");

            HasRequired(p => p.Endereco)
                .WithMany()
                .HasForeignKey(fk => fk.EnderecoId);

            
            Property(p => p.EnderecoId)
               .HasColumnName("EnderecoId")
               .HasColumnType("int")
               .IsRequired();
            Property(p => p.Numero)
                .HasColumnType("Numero")
                .HasColumnType("char")
                .HasMaxLength(6)
                .IsRequired();
            
        }
    }
}
