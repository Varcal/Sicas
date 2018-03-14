using Domain.Financeiro.Entities.DespesasSeguradora;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class DespesaSeguradoraItemMap: BaseMap<DespesaSeguradoraItem>
    {
        public DespesaSeguradoraItemMap()
        {
            ToTable("DespesaSeguradoraItem");

            Property(p => p.DespesaSeguradoraId).IsRequired();
            Property(p => p.Data).HasColumnType("date").IsRequired();
            Property(p => p.Endereco).HasMaxLength(200).IsRequired();
            Property(p => p.KmPercorrido).HasPrecision(18, 2).IsRequired();
            Property(p => p.TransportePedagio).HasPrecision(18, 2).IsRequired();
            Property(p => p.HospedagemRefeicao).HasPrecision(18, 2).IsRequired();
            Property(p => p.Total).HasPrecision(18, 2).IsRequired();
        }
    }
}
