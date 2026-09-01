using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WeeklyAssignment.Filters;

public class ActionExecutionLoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<ActionExecutionLoggingFilter> _logger;

    public ActionExecutionLoggingFilter(ILogger<ActionExecutionLoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();

        var controllerName = context.Controller.GetType().Name;

        var actionName = context.ActionDescriptor.RouteValues["action"];

        _logger.LogInformation("Starting {Controller}.{Action}", controllerName, actionName);

        try
        {
            await next();
        }
        finally
        {
            stopwatch.Stop();

            _logger.LogInformation("Finished {Controller}.{Action} in {ElapsedMilliseconds} ms", controllerName, actionName, stopwatch.ElapsedMilliseconds);
        }
    }
}