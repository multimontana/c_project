using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class FloorEntityConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            // Primary Key
            builder.HasKey(p => p.Id);

            // Indexes
            builder.HasIndex(e => e.ImobjId)
                .HasName("IX_Floor_IMObjID");

            builder.HasIndex(e => e.BuildingId)
                .HasName("IX_Floor_BuildingID");

            // Properties
            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("Этаж");
            builder.Property(p => p.Id)
                .HasColumnName("Идентификатор");

            builder.Property(p => p.Name)
                .HasColumnName("Название")
                .HasMaxLength(255);

            builder.Property(e => e.FloorDrawing)
                .HasColumnName("Чертеж этажа")
                .HasMaxLength(255);

            builder.Property(p => p.Note).HasColumnName("Примечание")
                .HasColumnType("ntext");

            builder.Property(e => e.MethodNamesofRooms)
                .HasColumnName("Способ именования комнат")
                .HasMaxLength(20);

            builder.Property(e => e.BuildingId)
                .HasColumnName("ИД здания");

            builder.Property(e => e.VisioId)
                .HasColumnName("Visio_ID");

            builder.Property(e => e.Vsdfile)
                .HasColumnName("VSDFile").HasColumnType("image");

            builder.Property(e => e.ExternalId)
                .IsRequired().HasColumnName("ExternalID")
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.UnitID)
                .HasColumnName("ИД подразделения");

            builder.Property(e => e.ImobjId)
                .HasColumnName("IMObjID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.PeripheralDatabaseId)
                .HasColumnName("PeripheralDatabaseID");

            builder.Property(e => e.ComplementaryId)
                .HasColumnName("ComplementaryID");

            // Relationship
            builder.HasOne(d => d.BuildingNavigationId)
                .WithMany(p => p.Floors)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_Этаж_Здание");
        }
    }
}