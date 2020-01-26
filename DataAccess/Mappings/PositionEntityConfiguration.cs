using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

            // Table & Column Mapping
            builder.ToTable("Должности");
            builder.Property(e => e.Id).HasColumnName("Идентификатор");
            builder.Property(e => e.Name).HasColumnName("Название").HasMaxLength(255);
            builder.Property(e => e.ImobjId).HasColumnName("IMObjID");

            builder.Property(e => e.ImobjId)
                .HasColumnName("IMObjID")
                .HasDefaultValueSql("(newid())");
        }
    }
}