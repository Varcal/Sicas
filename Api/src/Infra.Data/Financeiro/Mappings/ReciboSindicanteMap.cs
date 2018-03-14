using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class ReciboSindicanteMap : BaseMap<ReciboSindicante>
    {
        public ReciboSindicanteMap()
        {
            ToTable("ReciboSindicante");

            HasMany(p => p.ReciboSindicanteItems)
                .WithRequired()
                .HasForeignKey(p => p.ReciboSindicanteId);

            Property(p => p.Id).HasColumnName("NumeroRecibo").IsRequired();
            Property(p => p.Sindicante).HasMaxLength(200).IsRequired();
            Property(p => p.Cpf).HasMaxLength(11).IsRequired();
            Property(p => p.DadosBancarios).HasMaxLength(100).IsRequired();
            Property(p => p.DataInicio).HasColumnType("date").IsRequired();
            Property(p => p.DataFim).HasColumnType("date").IsRequired();
        }
    }
}
