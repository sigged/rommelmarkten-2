using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure;
using Rommelmarkten.Api.Common.Web.Middlewares;
using Rommelmarkten.Api.Features.Affiliates;
using Rommelmarkten.Api.Features.Captchas;
using Rommelmarkten.Api.Features.FAQs;
using Rommelmarkten.Api.Features.Markets;
using Rommelmarkten.Api.Features.NewsArticles;
using Rommelmarkten.Api.Features.ShoppingLists;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.MigrationsAggregator;
using Rommelmarkten.Api.WebApi.Persistence;
using Rommelmarkten.Api.WebApi.Services;
using Rommelmarkten.Api.WebApi.Versioning;

namespace Rommelmarkten.Api.WebApi
{
    /// <summary>
    /// Runs the web application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry method for the web application
        /// </summary>
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var services = builder.Services;

            //add features first
            builder.Services.AddAffiliateFeature(configuration);
            builder.Services.AddCaptchaFeature(configuration);
            builder.Services.AddFAQFeature(configuration);
            builder.Services.AddMarketFeature(configuration);
            builder.Services.AddNewsArticleFeature(configuration);
            builder.Services.AddShoppingListsFeature(configuration);
            builder.Services.AddUsersFeature(configuration);

            // Add services to the container.
            builder.Services.AddMigrations(configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddRealtimeMessaging(builder.Configuration);

            // Add aspnet services
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddTransient<ExceptionPresenter>();
            builder.Services.AddProblemDetails();

            builder.AddSwaggerSupportedVersioning();
            builder.Services.AddOpenApiDocument();

            try
            {
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseSwaggerSupportedVersioning();

                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseOpenApi(); //throws when "debug just my code" is disabled!! (sliced controller prob..(?))
                    app.UseSwaggerUi();
                }

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
                        var userDbContext = scopedServices.GetRequiredService<UsersDbContext>();
                        var shoppingListsDbContext = scopedServices.GetRequiredService<ShoppingListsDbContext>();
                        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                        if (userDbContext.Database.IsSqlServer())
                        {
                            userDbContext.Database.Migrate();
                        }

                        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, userDbContext);
                        await ApplicationDbContextSeed.SeedSampleDataAsync(shoppingListsDbContext);
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
