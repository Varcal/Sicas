using Domain.Financeiro.Entities.RecibosSindicantes;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Financeiro.Mappings
{
    public class LancamentoMap: BaseMap<Lancamento>
    {
        public LancamentoMap()
        {
            ToTable("Lancamento");

            HasKey(p => p.Id);

            
            Property(p => p.LancamentoTipo)
                .HasColumnName("LancamentoTipo")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            Property(p => p.Valor)
                .HasColumnName("Valor")
                .HasColumnType("decimal")
                .HasPrecision(11,2)
                .IsRequired();
            Property(p => p.TipoFinanceiro)
                .HasColumnName("TipoFinanceiro")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Observacao)
                .HasColumnName("Observacao")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsOptional();

        }
    }
}
