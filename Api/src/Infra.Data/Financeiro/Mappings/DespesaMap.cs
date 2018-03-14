using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class DespesaMap: BaseMap<Despesa>
    {
        public DespesaMap()
        {
            ToTable("Despesa");

            HasRequired(p => p.Origem)
                .WithMany()
                .HasForeignKey(fk => fk.EnderecoOrigemId);


            HasRequired(p => p.Destino)
                .WithMany()
                .HasForeignKey(fk => fk.EnderecoDestinoId);
                

            HasMany(p => p.DespesasAdicionais)
                .WithRequired()
                .HasForeignKey(p => p.DespesaId)
                .WillCascadeOnDelete(true);
                

            Property(p => p.Descricao)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(p => p.Data)
                .HasColumnType("date")
                .IsRequired();

            Property(p => p.Kms)
                .HasColumnName("Kms")
                .HasColumnType("decimal")
                .HasPrecision(5, 1)
                .IsRequired();

            Property(p => p.ValorKm)
                .HasColumnName("ValorKm")
                .HasColumnType("decimal")
                .HasPrecision(11, 2)
                .IsRequired();
        }
    }
}
