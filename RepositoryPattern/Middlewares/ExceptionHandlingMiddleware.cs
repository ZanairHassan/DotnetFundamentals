using Microsoft.AspNetCore.Http;

namespace RepositoryPattern.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request at the {Path}: ", context.Request.Path);
                if (context.Response.HasStarted)
                {
                    throw;
                }
                await HandleExceptionAsync(context);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.Clear();

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "text/html";

            await context.Response.WriteAsync(
                """
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Internal Server Error</title>
                </head>
                <body>
                    <h1>Something went wrong.</h1>
                    <p>Please try again later.</p>
                </body>
                </html>
                """);
        }
    }
}