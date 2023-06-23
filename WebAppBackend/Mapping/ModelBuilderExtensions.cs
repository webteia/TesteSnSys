using Microsoft.EntityFrameworkCore;
using WebAppBackend.Entidades;

namespace WebAppBackend.Mapping
{
    public static class ModelBuilderExtensions
    {
        public static void AddMapping<T>(this ModelBuilder modelBuilder, EntityTypeConfiguration<T> configuration) where T: BaseEntidade
        {
            configuration.Map(modelBuilder.Entity<T>());
        }
    }
}
