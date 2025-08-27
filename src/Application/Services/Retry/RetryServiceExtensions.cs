using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Application.Services.Retry;

internal static class RetryServiceExtensions
{
    internal static IServiceCollection AddRetryService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RetryPolicyOptions>(configuration.GetSection("RetryPolicy"));

        services.AddSingleton<RetryPolicy>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RetryPolicyOptions>>().Value;
            return new RetryPolicy(options.MaxAttempts, options.BaseDelayMs);
        });

        services.AddSingleton<RetryService>();

        return services;
    }

    internal static IServiceCollection AddRetryService(this IServiceCollection services, RetryPolicy retryPolicy)
    { 
        services.AddSingleton<RetryPolicy>(sp =>
        {
            return retryPolicy;
        });

        services.AddSingleton<RetryService>();

        return services;
    }
}