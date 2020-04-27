using System.Net;
using System.Xml.Linq;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Pokemon_Web_API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder builder, ILogger<Startup> logger)
        {
            builder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            logger.LogError($"Smthing went wrong: {contextFeature.Error}");
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal server error"
                            }.ToString());
                        }
                    });
            });
        }
    }
}