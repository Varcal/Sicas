using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class DespesaAdicionalMap: BaseMap<DespesaAdicional>
    {
        public DespesaAdicionalMap()
        {
            ToTable("DespesaAdicional");

            Property(p => p.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal")
                .HasPrecision(6, 2)
                .IsRequired();
        }
    }
}
