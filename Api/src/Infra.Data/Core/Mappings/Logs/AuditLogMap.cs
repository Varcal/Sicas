using System.Data.Entity.ModelConfiguration;
using TrackerEnabledDbContext.Common.Models;

namespace Infra.Data.Core.Mappings.Logs
{
    public class AuditLogMap: EntityTypeConfiguration<AuditLog>
    {
        public AuditLogMap()
        {
            ToTable("AuditLog");

            HasKey(x => x.AuditLogId);


            Property(p => p.AuditLogId)
                .HasColumnType("bigint");
            Property(p => p.UserName)
                .HasColumnType("nvarchar(MAX)");
            Property(x => x.EventDateUTC)
                .IsRequired();
            Property(x => x.EventType)
                .IsRequired();
            Property(p => p.TypeFullName)
                .HasColumnType("nvarchar")
                .HasMaxLength(512)
                .IsRequired();
            Property(p => p.RecordId)
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
