using System.Data.Entity.ModelConfiguration;
using Domain.Core.Entities;

namespace Infra.Data.Core.Mappings.Base
{
    public class BaseMap<T>: EntityTypeConfiguration<T> where T: EntityBase
    {
        public BaseMap()
        {

            Property(p => p.DataCadastro)
                .HasColumnName("DataCadastro")
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsRequired();
            Property(p => p.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("bit")
                .IsRequired();
        } 
    }
}
