using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data.Entity.ModelConfiguration;
using WebAppBackend.Entidades;

namespace WebAppBackend.Mapping
{
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("tb_customer");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");


            builder.Property(x => x.DataAtualizacao)
                .HasColumnType("timestamp")
                .IsRequired(false);

            base.Map(builder);
        }
    }
}
