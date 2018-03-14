using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Infra.Data.Core.Extensions
{
    internal static class TypeConfigurationExtensions
    {
        public static PrimitivePropertyConfiguration HasUniqueIndexAnnotation(this PrimitivePropertyConfiguration property, string indexName, int columnOrder)
        {
            var indexAttribute = new IndexAttribute(indexName, columnOrder) { IsUnique = true };
            var indexAnnotation = new IndexAnnotation(indexAttribute);

            return property.HasColumnAnnotation(IndexAnnotation.AnnotationName, indexAnnotation);
        }

        public static PrimitivePropertyConfiguration HasUniqueIndexAnnotation(this PrimitivePropertyConfiguration property, string indexName)
        {
            var indexAttribute = new IndexAttribute(indexName) { IsUnique = true };
            var indexAnnotation = new IndexAnnotation(indexAttribute);

            return property.HasColumnAnnotation(IndexAnnotation.AnnotationName, indexAnnotation);
        }
    }
}
