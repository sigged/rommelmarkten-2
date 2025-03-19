using Microsoft.Extensions.Options;
using Rommelmarkten.Api.WebApi.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rommelmarkten.Api.WebApi.Versioning
{
    internal static class SwaggerVersioningExtensions
    {
        public static IHostApplicationBuilder AddSwaggerSupportedVersioning(this IHostApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning().AddMvc().AddApiExplorer();

            if (builder.Environment.IsDevelopment())
            {
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
                builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            }

            return builder;
        }

        internal static IApplicationBuilder UseSwaggerSupportedVersioning(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in app.DescribeApiVersions())
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName);
                        }
                    });
            }

            return app;
        }
    }
}
