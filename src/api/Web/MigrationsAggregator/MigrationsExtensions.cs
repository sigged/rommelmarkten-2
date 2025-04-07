using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Rommelmarkten.Api.MigrationsAggregator
{
    public static class MigrationsExtensions
    {

        public static void AddMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MigrationsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly(typeof(MigrationsDbContext).Assembly.FullName)
                )
            );
        }
    }
}
