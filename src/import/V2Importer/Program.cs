using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Infrastructure.Persistence;

namespace V2Importer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Rommelmarkten Data Import Tool");
            Console.WriteLine("------------------------------");
            Console.WriteLine();


            var host = CreateHostBuilder(args).Build();

            // Resolve and run your application
            using var scope = host.Services.CreateScope();
            var importer = scope.ServiceProvider.GetRequiredService<Importer>();

            var eventService = new ImporterDomainEventService();
            var importerDatetime = new ImporterDatetime();
            var currentUser = new ImportCurrentUserService();

            await importer.Import();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IDomainEventService, ImporterDomainEventService>();
                    services.AddSingleton<IDateTime, ImporterDatetime>();
                    services.AddSingleton<ICurrentUserService, ImportCurrentUserService>();

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(Importer.targetDatabase);
                    });
                    services.AddSingleton<Importer>();
                });
    }
}
