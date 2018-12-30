using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoPay.Infrastructure.Managers;

namespace AutoPay.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //get host
            var host = CreateWebHostBuilder(args).Build();
            //run seeder
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Data seeding started.");
                try
                {
                    var dataSeedManager = services.GetRequiredService<IDataSeedManager>();
                    dataSeedManager.InitializeAsync().Wait();
                    logger.LogInformation("Data seeding completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Unable to run seeder. \n{ex}");
                }
            }
            //run host
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .UseStartup<Startup>();
    }
}
