using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Extensions;
using Ordering.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Ordering.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
               .Build()
               .MigrateDatabase<OrderContext>(async (context, services) =>
               {
                   var logger = services.GetService<ILogger<OrderContextSeed>>();
                   await OrderContextSeed.SeedAsync(context, logger);
               })
               .RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}