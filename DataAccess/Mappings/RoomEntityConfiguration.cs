using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class RoomEntityConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            //Primary key
            builder.HasKey(e => e.Id);

            // Indexes
            builder.HasIndex(e => e.ImobjId)
                .HasName("IX_Room_IMObjID");

            builder.HasIndex(e => e.FloorId)
                .HasName("IX_Room_FloorID");

            builder.Property(e => e.Id).ValueGeneratedNever();

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

            builder.Property(e => e.VisioId).HasColumnName("Visio_ID");

            builder.Property(e => e.SubdivisionId).HasColumnName("ИД подразделения");

            builder.Property(e => e.TypeId).HasColumnName("ИД типа");

            builder.Property(e => e.FloorId).HasColumnName("ИД этажа");

            builder.Property(e => e.Key).HasMaxLength(50);

            builder.Property(e => e.Name).HasColumnName("Название").HasMaxLength(255);

            builder.Property(e => e.ServicedZone)
                .HasColumnName("Обслуживаемая зона")
                .HasMaxLength(50);

            builder.Property(e => e.Note).HasColumnName("Примечание").HasMaxLength(255);

            builder.Property(e => e.Size).HasColumnName("Размер").HasMaxLength(50);

            builder.Property(e => e.Location)
                .HasColumnName("Точка расположения")
                .HasMaxLength(50);

            builder.Property(e => e.Drawing).HasMaxLength(255);

            builder.HasOne(d => d.FloorNavigationId)
                .WithMany(p => p.Rooms)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("FK_Комната_Этаж");
        }
    }
}
