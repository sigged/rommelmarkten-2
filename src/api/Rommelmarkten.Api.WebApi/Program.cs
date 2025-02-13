

using Microsoft.Extensions.Options;
using Rommelmarkten.Api.WebApi.Filters;
using Rommelmarkten.Api.WebApi.Versioning;
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

            builder.AddSwaggerSupportedVersioning();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwaggerSupportedVersioning();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
