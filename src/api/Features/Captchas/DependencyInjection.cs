using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Captchas.Application.Gateways;
using Rommelmarkten.Api.Features.Captchas.Infrastructure.Captcha;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Captchas
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddCaptchaFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



            //add altcha captcha
            //services.AddAltcha(configuration);
            services.AddScoped<ICaptchaProvider, NullCaptchaProvider>();


            return services;
        }
    }
}