using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Indexes
            builder.HasIndex(e => e.ImobjId)
                .HasName("IX_User_IMObjID");

            builder.HasIndex(e => e.WorkspaceId)
                .HasName("IX_User_WorkplaceID");

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Admin)
                .IsRequired()
                .HasDefaultValueSql("(0)");

            // Table & Column Mapping
            builder.ToTable("Пользователи");
            builder.Property(e => e.Id).HasColumnName("Идентификатор");
            builder.Property(e => e.CalendarWorkScheduleId).HasColumnName("CalendarWorkScheduleID");

            builder.Property(e => e.ComplementaryGuidId).HasColumnName("ComplementaryGuidID");

            builder.Property(e => e.ComplementaryId).HasColumnName("ComplementaryID");

            builder.Property(e => e.Email).HasMaxLength(100);

            builder.Property(e => e.ExternalId)
                .IsRequired()
                .HasColumnName("ExternalID")
                .HasMaxLength(250)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.ImobjId)
                .HasColumnName("IMObjID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.IsLockedForOsi).HasColumnName("IsLockedForOSI");

            builder.Property(e => e.LoginName)
                .HasColumnName("Login name")
                .HasMaxLength(100);

            builder.Property(e => e.NetworkAdmin)
                .IsRequired()
                .HasDefaultValueSql("(0)");
            
            builder.Property(e => e.Number).HasMaxLength(100);

            builder.Property(e => e.PeripheralDatabaseId).HasColumnName("PeripheralDatabaseID");

            builder.Property(e => e.Removed)
                .IsRequired()
                .HasDefaultValueSql("(0)");

            builder.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Property(e => e.SdwebAccessIsGranted).HasColumnName("SDWebAccessIsGranted");

            builder.Property(e => e.SdwebPassword)
                .HasColumnName("SDWebPassword")
                .HasMaxLength(250);

            builder.Property(e => e.Sid)
                .HasColumnName("SID")
                .HasMaxLength(255)
                .HasDefaultValueSql("('')");

            builder.Property(e => e.SupportAdmin)
                .IsRequired()
                .HasDefaultValueSql("(0)");

            builder.Property(e => e.SupportEngineer)
                .IsRequired()
                .HasDefaultValueSql("(0)");

            builder.Property(e => e.SupportOperator)
                .IsRequired()
                .HasDefaultValueSql("(0)");

            builder.Property(e => e.TimeZoneId)
                .HasColumnName("TimeZoneID")
                .HasMaxLength(250);

            builder.Property(e => e.VisioId).HasColumnName("Visio_ID");

            builder.Property(e => e.PositionId).HasColumnName("ИД должности");

            builder.Property(e => e.RoomId).HasColumnName("ИД комнаты");

            builder.Property(e => e.SubdivisionId).HasColumnName("ИД подразделения");

            builder.Property(e => e.WorkspaceId).HasColumnName("ИД рабочего места");

            builder.Property(e => e.Name).HasColumnName("Имя").HasMaxLength(100);

            builder.Property(e => e.Initials).HasColumnName("Инициалы").HasMaxLength(30);

            builder.Property(e => e.Patronymic).HasColumnName("Отчество").HasMaxLength(100);

            builder.Property(e => e.Pager).HasColumnName("Пейджер").HasMaxLength(100);

            builder.Property(e => e.Notes).HasColumnName("Примечания").HasMaxLength(100);
            
            builder.Property(e => e.Phone).HasColumnName("Телефон").HasMaxLength(100);

            builder.Property(e => e.Phone1).HasColumnName("Телефон1").HasMaxLength(15);

            builder.Property(e => e.Phone2).HasColumnName("Телефон2").HasMaxLength(15);

            builder.Property(e => e.Phone3).HasColumnName("Телефон3").HasMaxLength(15);

            builder.Property(e => e.Phone4).HasColumnName("Телефон4").HasMaxLength(15);

            builder.Property(e => e.Fax).HasColumnName("Факс").HasMaxLength(100);

            builder.Property(e => e.Lastname).HasColumnName("Фамилия").HasMaxLength(100);

            builder.Property(e => e.Photo).HasColumnName("Фото").HasColumnType("image");

            builder.HasOne(d => d.CalendarWorkSchedule)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CalendarWorkScheduleId)
                .HasConstraintName("FK_User_CalendarWorkSchedule");

            builder.HasOne(d => d.TimeZone)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.TimeZoneId)
                .HasConstraintName("FK_User_TimeZone");

            builder.HasOne(d => d.PositionNavigation)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Пользователи_Должности");

            builder.HasOne(d => d.Workspace)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.WorkspaceId)
                .HasConstraintName("FK_Пользователи_Рабочее место");
        }
    }
}
