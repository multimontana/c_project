namespace InfraManager.WebApi.DAL.Mappings
{
    using InfraManager.WebApi.DAL.DTOs;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CallEntityConfiguration : IEntityTypeConfiguration<Call>
    {
        public void Configure(EntityTypeBuilder<Call> builder)
        {
            // Indexes
            builder.HasIndex(e => e.Number);

            // Properties
            builder.Property(e => e.ClientFullName)
                .HasMaxLength(250);

            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            builder.Property(e => e.CallSummaryName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.PriorityId)
                .HasColumnName("PriorityID");

            builder.Property(e => e.EntityStateName)
                .HasMaxLength(250);

            builder.Property(e => e.UtcDateRegistered)
                .HasColumnType("datetime");

            builder.Property(e => e.UtcDateOpened)
                .HasColumnType("datetime");

            builder.Property(e => e.UtcDateClosed)
                .HasColumnType("datetime");

            builder.Property(e => e.ServiceItemFullName)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.AccomplisherFullName)
                .HasMaxLength(250);

            builder.Property(e => e.Htmldescription)
                .IsRequired()
                .HasColumnName("Description");

            builder.Property(e => e.Htmlsolution)
                .IsRequired()
                .HasColumnName("Solution");

            // Table & Column Mapping
            builder.ToTable("view_Call");
        }
    }
}