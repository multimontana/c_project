namespace CRUD
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    using Serilog;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

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