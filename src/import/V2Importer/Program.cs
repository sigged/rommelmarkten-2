using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Infrastructure.Persistence;
using Rommelmarkten.Api.Infrastructure.Services;
using V2Importer.Importers;

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
            var importer = scope.ServiceProvider.GetRequiredService<App>();

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
                        options.UseSqlServer(App.targetDatabase);
                    });
                    services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
                    services.AddScoped<IEntityRepository<MarketConfiguration>, EFRepository<MarketConfiguration>>();
                    services.AddScoped<IEntityRepository<MarketTheme>, EFRepository<MarketTheme>>();
                    services.AddScoped<IEntityRepository<AffiliateAd>, EFRepository<AffiliateAd>>();
                    services.AddScoped<IEntityRepository<BannerType>, EFRepository<BannerType>>();
                    services.AddScoped<IEntityRepository<NewsArticle>, EFRepository<NewsArticle>>();
                    services.AddScoped<IEntityRepository<FAQCategory>, EFRepository<FAQCategory>>();
                    services.AddScoped<IEntityRepository<FAQItem>, EFRepository<FAQItem>>();

                    services.AddSingleton<App>();
                    services.AddSingleton<UserImporter>();
                });
    }
}
