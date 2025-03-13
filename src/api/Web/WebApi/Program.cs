using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure;
using Rommelmarkten.Api.Common.Infrastructure.Identity;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Affiliates;
using Rommelmarkten.Api.Features.Affiliates.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Captchas;
using Rommelmarkten.Api.Features.FAQs;
using Rommelmarkten.Api.Features.Markets;
using Rommelmarkten.Api.Features.NewsArticles;
using Rommelmarkten.Api.Features.ShoppingLists;
using Rommelmarkten.Api.Features.Users;
using Rommelmarkten.Api.WebApi.Middlewares;
using Rommelmarkten.Api.WebApi.Persistence;
using Rommelmarkten.Api.WebApi.Services;
using Rommelmarkten.Api.WebApi.Versioning;

namespace Rommelmarkten.Api.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var configuration = builder.Configuration;
            var services = builder.Services;

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("RommelmarktenInMemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Stores.ProtectPersonalData = false; //todo: true!
                    options.Lockout.MaxFailedAccessAttempts = 5; //default
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //default
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddApiEndpoints();



            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddRealtimeMessaging(builder.Configuration);

            //add features
            builder.Services.AddAffiliateFeature(configuration);
            builder.Services.AddCaptchaFeature(configuration);
            builder.Services.AddFAQFeature(configuration);
            builder.Services.AddMarketFeature(configuration);
            builder.Services.AddNewsArticleFeature(configuration);
            builder.Services.AddShoppingListsFeature(configuration);
            builder.Services.AddUsersFeature(configuration);




            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddTransient<ExceptionPresenter>();
            builder.Services.AddProblemDetails();

            builder.AddSwaggerSupportedVersioning();

            var rigs = builder.Services.Select(s => s.ServiceType.FullName).ToList();

            try
            {
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseSwaggerSupportedVersioning();

                app.UseHttpsRedirection();


                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMiddleware<ExceptionPresenter>();

                app.UseOutputCache();

                app.MapControllers();

                app.MapGroup("/account")
                    .MapIdentityApi<ApplicationUser>();
                //.MapToApiVersion(new ApiVersion(1.0));


                using (var scope = app.Services.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    try
                    {
                        var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                        var affiliate = scopedServices.GetRequiredService<AffiliatesDbContext>();
                        var a = affiliate.AffiliateAds.FirstOrDefault();

                        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                        if (context.Database.IsSqlServer())
                        {
                            context.Database.Migrate();
                        }

                        //await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, context);
                        //await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                    }
                    catch (Exception ex)
                    {
                        var logger = scopedServices.GetRequiredService<ILogger<Program>>();

                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }

                await app.RunAsync();
            }
            catch(AggregateException aggregateException)
            {
                var exceptions = aggregateException.InnerExceptions;
                foreach(var exception in exceptions)
                {
                    Console.WriteLine(exception.Message);
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                }
                throw;
            }

        }
    }
}
