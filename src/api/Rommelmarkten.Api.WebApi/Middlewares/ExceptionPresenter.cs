﻿using Microsoft.AspNetCore.Mvc;
using Rommelmarkten.Api.Application.Common.Exceptions;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rommelmarkten.Api.WebApi.Middlewares
{
    /// <summary>
    /// Middleware to present exceptions as structured HTTP responses
    /// </summary>
    internal sealed class ExceptionPresenter : IMiddleware
    {
        private readonly ILogger<ExceptionPresenter> logger;
        private static JsonSerializerOptions jsonSerializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public ExceptionPresenter(ILogger<ExceptionPresenter> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var problemDetails = GetProblemDetails(exception);
            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            string json = problemDetails switch
            {
                ValidationProblemDetails validationProblemDetails => JsonSerializer.Serialize(validationProblemDetails, jsonSerializationOptions),
                ExceptionProblemDetails exceptionProblemDetails => JsonSerializer.Serialize(exceptionProblemDetails, jsonSerializationOptions),
                _ => JsonSerializer.Serialize(problemDetails, jsonSerializationOptions),
            };

            await httpContext.Response.WriteAsync(json);
        }

        private static ProblemDetails GetProblemDetails(Exception exception) =>
            exception switch
            {
                NotFoundException notFoundException => new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = notFoundException.Message,
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.5"
                },
                ValidationException validationException => new ValidationProblemDetails
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Title = "Validation Failed",
                    Detail = validationException.Message,
                    Errors = validationException.Errors,
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.21"
                },
                ForbiddenAccessException forbiddenException => new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = forbiddenException.Message,
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.4"
                },
                UnauthorizedAccessException notunauthorizedException => new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = notunauthorizedException.Message,
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.5.2"
                },
                ApplicationException applicationException => new ExceptionProblemDetails(exception)
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Application Error",
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1"
                },
                _ => new ExceptionProblemDetails(exception)
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1"
                }
            };
    }

    public class ExceptionProblemDetails : ProblemDetails
    {
        public ExceptionProblemDetails(Exception exception)
        {
#if DEBUG
            Exception? currentException = exception;
            do
            {
                Detail = currentException.Message;
                Extensions["stackTrace"] = currentException.StackTrace;
                currentException = currentException.InnerException;
            }
            while (currentException != null);    
#endif
        }
    }
}
