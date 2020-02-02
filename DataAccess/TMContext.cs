namespace DataAccess
{
    using DataAccess.DTOs;

    using Log;
    using Mappings;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The context.
    /// </summary>
    public class TmContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TmContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public TmContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        }

        public DbSet<User> Users { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CallEntityConfiguration());
        }

        /// <summary>
        /// Set log factory
        /// </summary>
        private static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            // log provider
            builder.AddProvider(new LoggerProvider());
        });
    }
}