using Microsoft.AspNetCore.Http;

namespace WeeklyAssignment.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception occurred while processing {Method} {Path}", context.Request.Method, context.Request.Path);

        if (context.Response.HasStarted)
        {
            _logger.LogWarning("The response has already started. " + "The exception cannot be handled by the middleware.");

            throw exception;
        }

        context.Response.Clear();

        context.Response.Redirect("/error");
    }
}