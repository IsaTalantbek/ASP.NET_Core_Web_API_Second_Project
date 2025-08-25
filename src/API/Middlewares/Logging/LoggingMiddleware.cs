namespace API.Middlewares.Logging;

public class LoggingMiddleware : IMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var requestId = Guid.NewGuid();
        using (_logger.BeginScope($"[{requestId}]"))
        {
            _logger.LogInformation("Request path: {Path}", context.Request.Path);
            await next(context);
        }
    }
}
