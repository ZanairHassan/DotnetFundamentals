using System.Diagnostics;

namespace ASP.NetFundamentals.Middlewares
{
    public sealed class RequestLoggingMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var correlationId = GetOrCreateCorrelationId(context);

            context.Response.Headers[CorrelationIdHeader] = correlationId;

            _logger.LogInformation( "Request started. CorrelationId: {CorrelationId}, Method: {Method}", correlationId, context.Request.Method);

            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Request failed. CorrelationId: {CorrelationId}, Method: {Method}", correlationId, context.Request.Method);

                throw;
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "Request completed. CorrelationId: {CorrelationId}, Method: {Method},  StatusCode: {StatusCode}, Duration: {Duration}ms",
                    correlationId,
                    context.Request.Method,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
        }

        private static string GetOrCreateCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var existingCorrelationId) && !string.IsNullOrWhiteSpace(existingCorrelationId))
            {
                return existingCorrelationId.ToString();
            }

            return Guid.NewGuid().ToString();
        }
    }
}
