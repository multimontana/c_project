namespace InfraManager.WebApi
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    using Serilog;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// The create host builder.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="IHostBuilder"/>.
        /// </returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                    {
                        config.ReadFrom.Configuration(context.Configuration);
                        config.Enrich.FromLogContext();
                    })
                .ConfigureWebHostDefaults(
                    webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });
    }
}