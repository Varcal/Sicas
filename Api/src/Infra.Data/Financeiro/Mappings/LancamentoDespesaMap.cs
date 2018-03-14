using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class LancamentoDespesaMap: BaseMap<LancamentoDespesa>
    {
        public LancamentoDespesaMap()
        {
            ToTable("LancamentoDespesa");

            HasRequired(p => p.Despesa)
                .WithMany()
                .HasForeignKey(fk => fk.DespesaId);

            Property(p => p.DespesaId)
                .HasColumnName("DespesaId")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
