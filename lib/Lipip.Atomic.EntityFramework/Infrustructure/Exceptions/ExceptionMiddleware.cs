using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Infrustructure.Exceptions
{
    public class ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
         IWebHostEnvironment env)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred for request {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new ExceptionResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = env.IsDevelopment()
                        ? ex.Message
                        : "An unexpected error occurred.",
                    Details = env.IsDevelopment()
                        ? ex.InnerException?.Message
                        : null,
                    StackTrace = env.IsDevelopment()
                        ? ex.StackTrace
                        : null
                };

                var json = JsonSerializer.Serialize(response,
                    new JsonSerializerOptions { WriteIndented = true });

                await context.Response.WriteAsync(json);
            }
        }
    }
}
