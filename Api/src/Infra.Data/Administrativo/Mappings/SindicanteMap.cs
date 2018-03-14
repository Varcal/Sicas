using Domain.Administrativo.Entities;
using Infra.Data.Core.Extensions;
using Infra.Data.Core.Mappings.Base;

namespace Infra.Data.Administrativo.Mappings
{
    public class SindicanteMap: BaseMap<Sindicante>
    {
        public SindicanteMap()
        {
            ToTable("Sindicante");

            HasMany(p => p.Enderecos)
                .WithRequired()
                .HasForeignKey(fk => fk.SindicanteId);
            HasMany(p => p.Honorarios)
                .WithRequired()
                .HasForeignKey(fk => fk.SindicanteId);
            HasRequired(p => p.DadosBancarios)
                .WithMany()
                .HasForeignKey(fk => fk.DadosBancariosId);
            HasRequired(p => p.DadosPessoaFisica)
                .WithMany()
                .HasForeignKey(fk=>fk.DadosPessoaFisicaId);
           
            
            Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .HasUniqueIndexAnnotation("IX_Email_Unique")
                .IsRequired();
            Property(p => p.Telefone)
                .HasColumnName("Telefone")
                .HasColumnType("char")
                .HasMaxLength(10)
                .HasUniqueIndexAnnotation("IX_Telefone_Unique")
                .IsRequired();
            Property(p => p.Celular)
                .HasColumnName("Celular")
                .HasColumnType("char")
                .HasMaxLength(11)
                .HasUniqueIndexAnnotation("IX_Celular_Unique")
                .IsRequired();
            Property(p => p.DadosBancariosId)
                .HasColumnName("DadosBancariosId")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
