

using Microsoft.Extensions.Options;
using Rommelmarkten.Api.WebApi.Filters;
using Rommelmarkten.Api.WebApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rommelmarkten.Api.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddProblemDetails();

            builder.Services.AddApiVersioning().AddMvc().AddApiExplorer();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
                builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            }
                

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
