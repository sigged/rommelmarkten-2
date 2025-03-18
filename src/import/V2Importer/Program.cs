using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using Rommelmarkten.Api.Features.Affiliates.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.FAQs.Domain;
using Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Markets.Domain;
using Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.NewsArticles.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Services;
using Rommelmarkten.Api.MigrationsAggregator;
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
                .ConfigureLogging(builder => {
                    builder.AddConsole();
                    builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IDomainEventService, ImporterDomainEventService>();
                    services.AddSingleton<IDateTime, ImporterDatetime>();
                    services.AddSingleton<ICurrentUserService, ImportCurrentUserService>();

                    //services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(App.targetDatabase); });
                    //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


                    services.AddDbContext<MigrationsDbContext>(options => options.UseSqlServer((App.targetDatabase)));
                    


                    services.AddScoped<IEntityRepository<UserProfile>, EFRepository<UserProfile, MigrationsDbContext>>();

                    services.AddScoped<IEntityRepository<AffiliateAd>, EFRepository<AffiliateAd, MigrationsDbContext>>();

                    services.AddScoped<IEntityRepository<BannerType>, EFRepository<BannerType, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<FAQCategory>, EFRepository<FAQCategory, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<FAQItem>, EFRepository<FAQItem, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<Province>, EFRepository<Province, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketConfiguration>, EFRepository<MarketConfiguration, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketTheme>, EFRepository<MarketTheme, MigrationsDbContext>>();

                    services.AddScoped<IEntityRepository<Market>, EFRepository<Market, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketDate>, EFRepository<MarketDate, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketInvoiceLine>, EFRepository<MarketInvoiceLine, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketInvoiceReminder>, EFRepository<MarketInvoiceReminder, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketInvoice>, EFRepository<MarketInvoice, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketRevision>, EFRepository<MarketRevision, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketRevisionWithTheme>, EFRepository<MarketRevisionWithTheme, MigrationsDbContext>>();
                    services.AddScoped<IEntityRepository<MarketWithTheme>, EFRepository<MarketWithTheme, MigrationsDbContext>>();

                    services.AddSingleton<App>();
                    services.AddSingleton<Importer>();
                });
    }
}
