using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppBackend.Entidades;

namespace WebAppBackend.Mapping
{
    public class EntityTypeConfiguration<T> where T : BaseEntidade
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DataCadastro)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
