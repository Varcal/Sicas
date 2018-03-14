using Domain.Administrativo.Entities;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class SeguradoraMap: BaseMap<Seguradora>
    {
        public SeguradoraMap()
        {
            ToTable("Seguradora");

            //HasMany(p => p.Eventos)
            //    .WithMany(p => p.Seguradoras)
            //    .Map(map =>
            //    {
            //        map.ToTable("SeguradoraTipoServico");
            //        map.MapLeftKey("SeguradoraId");
            //        map.MapRightKey("TipoServicoId");
            //    });

            HasMany(p => p.Eventos)
                .WithRequired()
                .HasForeignKey(fk => fk.SeguradoraId);
            HasRequired(p => p.DadosPessoaJuridica)
                .WithMany()
                .HasForeignKey(fk => fk.DadosPessoaJuridicaId);
            
            Property(p => p.Telefone)
               .HasColumnName("Telefone")
               .HasColumnType("char")
               .HasMaxLength(10)
               .HasUniqueIndexAnnotation("IX_Telefone_Unique")
               .IsRequired();
            Property(p => p.Contato)
                .HasColumnName("Contato")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
            Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .HasUniqueIndexAnnotation("IX_Email_Unique")
                .IsRequired();
            Property(p => p.ValorPorKm)
                .HasColumnName("ValorPorKm")
                .HasColumnType("decimal")
                .HasPrecision(9,2)
                .IsRequired();
            Property(p => p.UrlAdministrativo)
                .HasColumnName("UrlAdministrativo")
                .HasColumnType("varchar")
                .HasMaxLength(256)
                .IsRequired();
            Property(p => p.UrlFinanceiro)
                .HasColumnName("UrlFinanceiro")
                .HasColumnType("varchar")
                .HasMaxLength(256)
                .IsRequired();
            Property(p => p.Observacoes)
                .HasColumnName("Observacoes")
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsOptional();
        }
    }
}
