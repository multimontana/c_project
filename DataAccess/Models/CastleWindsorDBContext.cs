using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class CastleWindsorDBContext : DbContext
    {
        public CastleWindsorDBContext()
        {
        }

        public CastleWindsorDBContext(DbContextOptions<CastleWindsorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CastleWindsorDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepId)
                    .HasName("PK_dbo.department");

                entity.ToTable("department");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.DepLocation).HasColumnName("dep_location");

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasColumnName("dep_name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_dbo.employee");

                entity.ToTable("employee");

                entity.HasIndex(e => e.DepId)
                    .HasName("IX_dep_id");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.EmpName)
                    .IsRequired()
                    .HasColumnName("emp_name")
                    .HasMaxLength(50);

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasColumnName("job_name");

                entity.Property(e => e.Salary)
                    .IsRequired()
                    .HasColumnName("salary");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("FK_dbo.employee_dbo.department_dep_id");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
