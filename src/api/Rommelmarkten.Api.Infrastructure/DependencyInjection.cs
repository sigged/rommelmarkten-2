using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Infrastructure.Caching;
using Rommelmarkten.Api.Infrastructure.Identity;
using Rommelmarkten.Api.Infrastructure.Persistence;
using Rommelmarkten.Api.Infrastructure.Security;
using Rommelmarkten.Api.Infrastructure.Services;
using System.Reflection;

namespace Rommelmarkten.Api.Infrastructure
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
            if(tokenSettings == null)
                throw new ApplicationException("Jwt settings not found in configuration");

            services.AddSingleton(tokenSettings);

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
            services.AddScoped<IEntityRepository<MarketConfiguration>, EFRepository<MarketConfiguration>>();
            services.AddScoped<IEntityRepository<MarketTheme>, EFRepository<MarketTheme>>();
            services.AddScoped<IEntityRepository<AffiliateAd>, EFRepository<AffiliateAd>>();
            services.AddScoped<IEntityRepository<BannerType>, EFRepository<BannerType>>();

            services.AddScoped<IDomainEventService, DomainEventService>();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenManager, TokenManager>();
            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            services.AddTransient<IAvatarGenerator, AvatarGenerator>();

            var tokenValidationParmFactory = new TokenValidationParametersFactory(tokenSettings);
            services.AddSingleton<ITokenValidationParametersFactory>(tokenValidationParmFactory);
            var tokenValidationParms = tokenValidationParmFactory.GetDefaultValidationParameters();

            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })

            .AddJwtBearer(jwtOptions => {
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
                            (path.StartsWithSegments("/hubs/notifications")))
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