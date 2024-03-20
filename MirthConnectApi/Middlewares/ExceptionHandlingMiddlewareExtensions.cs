using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SWECVI.ApplicationCore.CustomExceptions;


namespace SWECVI.MirthConnectApi.Middlewares

{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env,
            ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    await HandleExceptionAsync(context, logger, true);
                });
            });
        }

        private static async Task HandleExceptionAsync(HttpContext context, ILogger logger, bool includeDetails)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandlerPathFeature?.Error is null)
                return;

            // ProblemDetails has it's own content type
            context.Response.ContentType = "application/problem+json";

            var error = exceptionHandlerPathFeature.Error;
            var title = includeDetails ? error.Message : "An error occurred";
            var details = includeDetails ? error.ToString() : null;
            var status = GetCustomStatusCode(error);

            if (status == 500)
            {
                logger.LogError($"Warning: error code {status}. {title}{details}");
            }
            else
            {
                logger.LogWarning($"Warning: error code {status}. {title}{details}");
            }

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = details
            };

            // This is often very handy information for tracing the specific request
            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
            problem.Extensions["traceId"] = traceId;

            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
            context.Response.StatusCode = status;
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }

        private static int GetCustomStatusCode(Exception error)
        {
            if (error is CustomException)
                return CustomException.StatusCode;
            if (error is CustomValidationException || error is ValidationException)
                return CustomValidationException.StatusCode;
            if (error is CustomForbidException)
                return CustomForbidException.StatusCode;
            if (error is CustomNotFoundException)
                return CustomNotFoundException.StatusCode;

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}