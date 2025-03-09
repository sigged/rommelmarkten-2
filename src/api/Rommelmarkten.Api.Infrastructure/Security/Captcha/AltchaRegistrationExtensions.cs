using Ixnas.AltchaNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Infrastructure.Security.Captcha
{
    public static class AltchaRegistrationExtensions
    {

        public static IServiceCollection AddAltcha(this IServiceCollection services, IConfiguration configuration)
        {
            var selfHostedKeyBase64 = configuration.GetValue<string>("Altcha::SelfHostedKey");
            selfHostedKeyBase64 = "INSERT_BASE64_KEY_HERE";

            var selfHostedKey = Convert.FromBase64String(selfHostedKeyBase64!);
            //var apiSecret = configuration.GetValue<string>("Altcha::ApiSecret");
            //apiSecret = "csec_INSERT_SECRET_HERE";

            // Add Altcha services.
            //services.AddScoped<IAltchaChallengeStore, AltchaChallengeStore>();

            //// Add HttpClient for machine-to-machine challenges (ignoring SSL issues for this example).
            //builder.Services.AddHttpClient(Options.DefaultName, c => { })
            //       .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            //       {
            //           ClientCertificateOptions = ClientCertificateOption.Manual,
            //           ServerCertificateCustomValidationCallback =
            //               (_, _, _, _) => true
            //       });

            services.AddScoped(sp => Altcha.CreateServiceBuilder()
                                                   .UseSha256(selfHostedKey)
                                                   .UseStore(sp.GetService<IAltchaChallengeStore>)
                                                   .SetExpiryInSeconds(5)
                                                   .Build());
            //services.AddScoped(sp => Altcha.CreateApiServiceBuilder()
            //                                       .UseApiSecret(apiSecret)
            //                                       .UseStore(sp.GetService<IAltchaChallengeStore>)
            //                                       .Build());
            services.AddScoped(_ => Altcha.CreateSolverBuilder()
                                                  .Build());

            services.AddScoped<ICaptchaProvider, AltchaProvider>();

            return services;
        }
    }
}
