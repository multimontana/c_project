namespace InfraManager.WebApi.DAL.Mappings
{
    using InfraManager.WebApi.DAL.DTOs;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentDto>
    {
        public void Configure(EntityTypeBuilder<DocumentDto> builder)
        {
            builder.HasNoKey();

            // Properties
            builder.Property(e => e.Data).HasColumnType("image");

            builder.Property(e => e.Extension).IsRequired().HasMaxLength(10).IsUnicode(false);

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(250).IsUnicode(false);

            // Table & Column Mapping
            builder.ToView("view_Document");
        }
    }
}