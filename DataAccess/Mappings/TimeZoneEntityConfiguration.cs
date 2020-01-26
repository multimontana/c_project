using System;
using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DataAccess.DTOs.TimeZone;

namespace DataAccess.Mappings
{
    class TimeZoneEntityConfiguration : IEntityTypeConfiguration<TimeZone>
    {
        public void Configure(EntityTypeBuilder<TimeZone> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .HasMaxLength(250);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
