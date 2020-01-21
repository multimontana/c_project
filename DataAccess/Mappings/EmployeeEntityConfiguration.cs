using DataAccess.DTOs;
using DataAccess.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Primary Key
            builder.HasKey(p => p.EmployeeId);

            // Properties
            builder.Property(p => p.EmployeeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            builder.ToTable("employee");
            builder.Property(p => p.EmployeeId).HasColumnName("emp_id");
            builder.Property(p => p.EmployeeName).HasColumnName("emp_name");
            builder.Property(p => p.JobName).HasColumnName("job_name");
            builder.Property(p => p.Salary).HasColumnName("salary");
            builder.Property(p => p.DepartmentId).HasColumnName("dep_id");


            builder.HasOne(d => d.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(d => d.DepartmentId);
            // Init Data
            builder.HasData(DataInitialization.GetEmployees());
        }
    }
}