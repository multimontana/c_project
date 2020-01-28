using DataAccess.Log;
using Microsoft.EntityFrameworkCore;
using DataAccess.Mappings;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class TmContext : DbContext
    {
        public TmContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        /// <summary>
        /// Set log factory
        /// </summary>
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            // log provider
            builder.AddProvider(new LoggerProvider());
        });

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