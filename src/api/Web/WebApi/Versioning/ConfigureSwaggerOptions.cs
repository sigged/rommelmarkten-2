using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Rommelmarkten.Api.Features.Affiliates.Web.V1;
using Rommelmarkten.Api.Features.Captchas.Web.V1;
using Rommelmarkten.Api.Features.FAQs.Web.V1;
using Rommelmarkten.Api.Features.Markets.Web.V1;
using Rommelmarkten.Api.Features.NewsArticles.Web.V1;
using Rommelmarkten.Api.Features.Users.Web.V1;
using Rommelmarkten.Api.WebApi.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Rommelmarkten.Api.WebApi.Versioning
{

    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

                options.IncludeXmlComments(typeof(ConfigureSwaggerOptions).Assembly, includeControllerXmlComments: true);
                options.IncludeXmlComments(typeof(AffiliateAdsController).Assembly, includeControllerXmlComments: true);
                //options.IncludeXmlComments(typeof(CaptchaController).Assembly, includeControllerXmlComments: true);
                //options.IncludeXmlComments(typeof(FAQCategoriesController).Assembly, includeControllerXmlComments: true);
                //options.IncludeXmlComments(typeof(MarketConfigurationsController).Assembly, includeControllerXmlComments: true);
                //options.IncludeXmlComments(typeof(NewsArticlesController).Assembly, includeControllerXmlComments: true);
                //options.IncludeXmlComments(typeof(UsersController).Assembly, includeControllerXmlComments: true);
            }

            // add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();
            options.EnableAnnotations();

            //define swagger auth button
            options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            //pass token to header
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        }
                    },
                    new List<string>()
                }
            });
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Rommelmarkten API",
                Version = description.ApiVersion.ToString(),
                Description = "Gateway API for rommelmarkten.be",
                Contact = new OpenApiContact() { Name = "sigged", Email = "sigged@users.noreply.github.com" },
                License = new OpenApiLicense
                {
                    Name = "SpruceBit (c) 2025 - All rights reserved - usage, copying and modifying strictly forbidden"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
