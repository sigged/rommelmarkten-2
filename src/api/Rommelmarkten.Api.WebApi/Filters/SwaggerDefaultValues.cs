﻿
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace Rommelmarkten.Api.WebApi.Filters
{

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse
                                  ? "default"
                                  : responseType.StatusCode.ToString();
                var response = operation.Responses[responseKey];

                foreach (var contentType in response.Content.Keys)
                {
                    if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
                    {
                        response.Content.Remove(contentType);
                    }
                }
            }

            if (operation.Parameters == null)
            {
                return;
            }

            //foreach (var parameter in operation.Parameters)
            //{
            //    var description = apiDescription.ParameterDescriptions
            //                                    .First(p => p.Name == parameter.Name);

            //    parameter.Description ??= description.ModelMetadata?.Description;

            //    if (parameter.Schema.Default == null && description.DefaultValue != null && description.ModelMetadata != null)
            //    {
            //        var json = JsonSerializer.Serialize(
            //            description.DefaultValue,
            //            description.ModelMetadata.ModelType);
            //        parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            //    }

            //    parameter.Required |= description.IsRequired;
            //}
        }
    }

    ///// <summary>
    ///// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    ///// </summary>
    ///// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
    ///// Once they are fixed and published, this class can be removed.</remarks>
    //public class SwaggerDefaultValues : IOperationFilter
    //{

    //    /// <summary>
    //    /// Applies the filter to the specified operation using the given context.
    //    /// </summary>
    //    /// <param name="operation">The operation to apply the filter to.</param>
    //    /// <param name="context">The current operation filter context.</param>
    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        var apiDescription = context.ApiDescription;

    //        operation.Deprecated |= apiDescription.IsDeprecated();

    //        if (operation.Parameters == null)
    //        {
    //            return;
    //        }

    //        foreach (var parameter in operation.Parameters)
    //        {
    //            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

    //            if (parameter.Description == null)
    //            {
    //                parameter.Description = description.ModelMetadata?.Description;
    //            }

    //            if (parameter.Schema.Default == null && description.DefaultValue != null)
    //            {
    //                parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
    //            }

    //            parameter.Required |= description.IsRequired;
    //        }
    //    }
    //}
}
