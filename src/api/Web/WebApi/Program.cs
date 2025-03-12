using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Users.Infrastructure.Identity;
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
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddRealtimeMessaging(builder.Configuration);

            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddTransient<ExceptionPresenter>();
            builder.Services.AddProblemDetails();

            builder.AddSwaggerSupportedVersioning();

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
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, context);
                    //await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await app.RunAsync();
        }
    }
}
