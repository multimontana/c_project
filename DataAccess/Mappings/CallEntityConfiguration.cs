namespace DataAccess.Mappings
{
    using DataAccess.DTOs;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CallEntityConfiguration : IEntityTypeConfiguration<Call>
    {
        public void Configure(EntityTypeBuilder<Call> builder)
        {
            // Indexes
            builder.HasIndex(e => e.Number);

            // Properties
            builder.Property(e => e.ClientFullName).HasMaxLength(250);

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedNever();

            builder.Property(e => e.CallSummaryName).IsRequired().HasMaxLength(250);

            builder.Property(e => e.PriorityId).HasColumnName("PriorityID");

            builder.Property(e => e.EntityStateName).HasMaxLength(250);

            builder.Property(e => e.UtcDateOpened).HasColumnType("datetime");

            // Table & Column Mapping
            builder.ToTable("view_Call");
        }
    }
}