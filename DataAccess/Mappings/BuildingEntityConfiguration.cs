using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class BuildingEntityConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes
            builder.HasIndex(e => e.ImobjId)
                .HasName("IX_Building_IMObjID");

            builder.HasIndex(e => e.OrganizationsId)
                .HasName("IX_Building_OrganizationID");

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

            // Table & Column Mappings
            builder.ToTable("Здание");

            builder.Property(e => e.Id)
                .HasColumnName("Идентификатор");

            builder.Property(e => e.Id)
                .HasColumnName("Название");

            builder.Property(e => e.Id)
                .HasColumnName("Индекс");

            builder.Property(e => e.Id)
                .HasColumnName("Город");

            builder.Property(e => e.Id)
                .HasColumnName("Район");

            builder.Property(e => e.Id)
                .HasColumnName("Корпус");

            builder.Property(e => e.Id)
                .HasColumnName("Изображение");

            builder.Property(e => e.ComplementaryId)
                .HasColumnName("ComplementaryID");

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasColumnName("ExternalID")
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.House)
                .HasMaxLength(7)
                .IsUnicode(false);

            builder.Property(e => e.ImobjId)
                .HasColumnName("IMObjID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.PeripheralDatabaseId).HasColumnName("PeripheralDatabaseID");

            builder.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Property(e => e.TimeZoneId)
                .HasColumnName("TimeZoneID")
                .HasMaxLength(250);

            builder.Property(e => e.VisioId).HasColumnName("Visio_ID");

            builder.Property(e => e.City).HasMaxLength(50);

            builder.Property(e => e.OrganizationsId).HasColumnName("ИД организации");

            builder.Property(e => e.UnitsId).HasColumnName("ИД подразделения");

            builder.Property(e => e.Image).HasMaxLength(255);

            builder.Property(e => e.Index).HasMaxLength(8);

            builder.Property(e => e.Case).HasMaxLength(2);

            builder.Property(e => e.Name).HasMaxLength(255);

            builder.Property(e => e.Region)
                .HasColumnName("Область-Край")
                .HasMaxLength(50);

            builder.Property(e => e.Note).HasColumnName("Примечание").HasMaxLength(255);

            builder.Property(e => e.District).HasColumnName("Район").HasMaxLength(50);

            builder.Property(e => e.Build).HasColumnName("Строение").HasMaxLength(5);

            builder.Property(e => e.WiringDiagram)
                .HasColumnName("Схема разводки")
                .HasMaxLength(255);

            builder.Property(e => e.Street).HasColumnName("Улица").HasMaxLength(50);

            // Relationship
            builder.HasOne(d => d.TimeZone)
                .WithMany(p => p.Buildings)
                .HasForeignKey(d => d.TimeZoneId)
                .HasConstraintName("FK_Building_TimeZone");
        }
    }
}