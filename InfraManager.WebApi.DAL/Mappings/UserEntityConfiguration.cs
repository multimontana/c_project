namespace InfraManager.WebApi.DAL.Mappings
{
    using InfraManager.WebApi.DAL.DTOs;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// The user entity configuration.
    /// </summary>
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserDto>
    {
        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties
            builder.Property(e => e.Id).ValueGeneratedNever();

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

            // Table & Column Mapping
            builder.ToTable("Пользователи");
        }
    }
}