using ASP.NetFundamentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ASP.NetFundamentals.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred while processing the MVC request.");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ViewResult
            {
                ViewName = "~/Views/Error/Index.cshtml",
                ViewData = new ViewDataDictionary<ErrorModel>(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    Model = new ErrorModel
                    {
                        RequestId = context.HttpContext.TraceIdentifier,
                        StatusCode = StatusCodes.Status500InternalServerError
                    }
                }
            };

            context.ExceptionHandled = true;
        }
    }
}