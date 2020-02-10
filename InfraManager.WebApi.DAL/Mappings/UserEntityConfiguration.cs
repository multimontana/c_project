namespace InfraManager.WebApi.DAL.Mappings
{
    using InfraManager.WebApi.DAL.DTOs;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

            // Table & Column Mapping
            builder.ToTable("Пользователи");

            builder.Property(e => e.Id)
                .HasColumnName("Идентификатор");

            builder.Property(e => e.LoginName)
                .HasColumnName("Login name")
                .HasMaxLength(100);

            builder.Property(e => e.Name)
                .HasColumnName("Имя")
                .HasMaxLength(100);

            builder.Property(e => e.SdwebPassword)
                .HasColumnName("SDWebPassword")
                .HasMaxLength(250);
        }
    }
}