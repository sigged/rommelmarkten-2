using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Caching;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRealtimeMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // configure strongly typed settings objects
            var jwtSettingsSection = configuration.GetRequiredSection("Settings:Jwt");
            services.Configure<TokenSettings>(jwtSettingsSection);
            var tokenSettings = jwtSettingsSection.Get<TokenSettings>();
            if (tokenSettings == null)
                throw new ApplicationException("Jwt settings not found in configuration");

            services.AddSingleton(tokenSettings);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    options.UseInMemoryDatabase("RommelmarktenInMemoryDb");
                }
                else
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                }
            });
    
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());


            //services
            //    .AddIdentity<ApplicationUser, IdentityRole>(options =>
            //    {
            //        options.SignIn.RequireConfirmedEmail = true;
            //        options.Stores.ProtectPersonalData = false; //todo: true!
            //        options.Lockout.MaxFailedAccessAttempts = 5; //default
            //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //default
            //        options.User.RequireUniqueEmail = true;
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders()
            //    .AddApiEndpoints();



            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            var tokenValidationParmFactory = new TokenValidationParametersFactory(tokenSettings);
            services.AddSingleton<ITokenValidationParametersFactory>(tokenValidationParmFactory);
            var tokenValidationParms = tokenValidationParmFactory.GetDefaultValidationParameters();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.ClaimsIssuer = tokenSettings.JwtIssuer;
                jwtOptions.TokenValidationParameters = tokenValidationParms;
                jwtOptions.SaveToken = true;
                jwtOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            //used to easily detect an expired token by the client
                            context.Response.Headers.Append("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },

                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/hubs/notifications"))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddApplicationAuthorization();


            //add caching
            services.AddScoped<ICacheManager, OutputCacheManager>();

            services.AddOutputCache(options =>
            {
                options.AddBasePolicy(builder =>
                            builder.Expire(TimeSpan.FromSeconds(60)));

                options.AddPolicy(CachePolicyNames.PublicMarkets, builder =>
                {
                    builder.Expire(TimeSpan.FromSeconds(60));
                    builder.Tag(new[] { CacheTagNames.Public, CacheTagNames.Markets });
                });
                options.AddPolicy(CachePolicyNames.OwnedMarkets, builder =>
                {
                    builder.Expire(TimeSpan.FromSeconds(60));
                    builder.Tag(new[] { CacheTagNames.Owned, CacheTagNames.Markets });
                });

                //options.UseStackExchangeRedis("your_redis_connection_string"); //uncomment to enable redis caching
            });


            return services;
        }
    }
}