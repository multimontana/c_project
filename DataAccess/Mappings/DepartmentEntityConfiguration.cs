using DataAccess.DTOs;
using DataAccess.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Primary Key
            builder.HasKey(p => p.DepartmentId);

            // Properties
            builder.Property(p => p.DepartmentName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mapping
            builder.ToTable("department");
            builder.Property(p => p.DepartmentId).HasColumnName("dep_id");
            builder.Property(p => p.DepartmentName).HasColumnName("dep_name");
            builder.Property(p => p.Location).HasColumnName("dep_location");

            // Init Data
            builder.HasData(DataInitialization.GetDepartment());
        }
    }
}