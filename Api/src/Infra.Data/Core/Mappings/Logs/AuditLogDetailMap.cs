using System.Data.Entity.ModelConfiguration;
using TrackerEnabledDbContext.Common.Models;

namespace Infra.Data.Core.Mappings.Logs
{
    public class AuditLogDetailMap : EntityTypeConfiguration<AuditLogDetail>
    {
        public AuditLogDetailMap()
        {
            ToTable("AuditLogDetail");

            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasColumnType("bigint");
            Property(p => p.PropertyName)
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();
            Property(p => p.NewValue)
                .HasColumnType("nvarchar(MAX)");
            Property(p => p.OriginalValue)
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
