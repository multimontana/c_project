using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class CalendarWorkScheduleEntityConfiguration : IEntityTypeConfiguration<CalendarWorkSchedule>
    {
        public void Configure(EntityTypeBuilder<CalendarWorkSchedule> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("ID")
                .ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Note)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Property(e => e.ShiftTemplate)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}