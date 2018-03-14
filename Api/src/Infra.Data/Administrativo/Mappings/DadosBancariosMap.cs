using Domain.Administrativo.Entities;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class DadosBancariosMap: BaseMap<DadosBancarios>
    {
        public DadosBancariosMap()
        {
            ToTable("DadosBancarios");

            HasRequired(p => p.Banco)
                .WithMany()
                .HasForeignKey(p => p.BancoId);

            Property(p => p.BancoId)
                .HasColumnName("BancoId")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.ContaTipo)
                .HasColumnName("ContaTipo")
                .HasColumnType("int")
                .IsRequired();
            Property(p => p.Agencia)
                .HasColumnName("Agencia")
                .HasColumnType("char")
                .HasMaxLength(4)
                .IsRequired();
            Property(p => p.Conta)
                .HasColumnName("Conta")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();
            Property(p => p.Digito)
                .HasColumnName("Digito")
                .HasColumnType("char")
                .HasMaxLength(1)
                .IsOptional();

        }
    }
}
