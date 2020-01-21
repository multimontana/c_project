using Microsoft.EntityFrameworkCore;
using DataAccess.Mappings;
namespace DataAccess
{
    public abstract class AppDbContext : DbContext
    {
        protected AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        }
    }
}