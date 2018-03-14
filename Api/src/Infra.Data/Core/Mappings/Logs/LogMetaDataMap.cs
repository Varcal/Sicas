using System.Data.Entity.ModelConfiguration;
using TrackerEnabledDbContext.Common.Models;

namespace Infra.Data.Core.Mappings.Logs
{
    public class LogMetaDataMap: EntityTypeConfiguration<LogMetadata>
    {
        public LogMetaDataMap()
        {
            ToTable("LogMetadata");

            HasKey(p => p.Id);

            Property(p => p.Key)
                .HasColumnType("nvarchar(MAX)");
            Property(p => p.Value)
                .HasColumnType("nvarchar(MAX)");
        }
    }
}
