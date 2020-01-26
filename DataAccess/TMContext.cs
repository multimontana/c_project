using Microsoft.EntityFrameworkCore;
using DataAccess.Mappings;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class TmContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public TmContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FloorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CalendarWorkScheduleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TimeZoneEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BuildingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PositionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WorkplaceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}