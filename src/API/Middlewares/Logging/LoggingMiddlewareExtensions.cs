namespace API.Middlewares.Logging;

public static class LoggingMiddlewareExtensions
{
    public static IServiceCollection AddLoggingMiddleware(this IServiceCollection services)
        => services.AddTransient<LoggingMiddleware>();

    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<LoggingMiddleware>();
}
