using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class WorkplaceEntityConfiguration : IEntityTypeConfiguration<Workplace>
    {
        public void Configure(EntityTypeBuilder<Workplace> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes
            builder.HasIndex(e => e.ImobjId)
                .HasName("IX_Workplace_IMObjID");


            builder.HasIndex(e => e.RoomId)
                .HasName("IX_Workplace_RoomID");

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

            // Table & Column Mapping
            builder.ToTable("Рабочее место");

            builder.Property(e => e.Id).HasColumnName("Идентификатор");
            builder.Property(e => e.ComplementaryId).HasColumnName("ComplementaryID");

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasColumnName("ExternalID")
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.ImobjId)
                .HasColumnName("IMObjID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.PeripheralDatabaseId).HasColumnName("PeripheralDatabaseID");

            builder.Property(e => e.RoomId).HasColumnName("ИД комнаты");

            builder.Property(e => e.UnitID).HasColumnName("ИД подразделения");

            builder.Property(e => e.Name).HasColumnName("Название").HasMaxLength(255);

            builder.Property(e => e.Note).HasColumnName("Примечание").HasMaxLength(255);

            builder.HasOne(d => d.RoomNavigationId)
                .WithMany(p => p.Workplaces)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Рабочее место_Комната");
        }
    }
}