using Microsoft.Extensions.Logging;

namespace Application.Services.Retry;

public class RetryService
{
    private readonly RetryPolicy _retryPolicy;
    private readonly ILogger<RetryService> _logger;

    public RetryService(RetryPolicy retryPolicy, ILogger<RetryService> logger)
    {
        _retryPolicy = retryPolicy;
        _logger = logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, Func<T, bool> shouldRetry, CancellationToken ct = default)
    {
        int attempt = 0;

        while (true)
        {
            attempt++;

            var result = await action();

            if (shouldRetry(result))
            {
                if (_retryPolicy.ShouldNextAttempt(attempt))
                {
                    await BackoffDelay(attempt, ct);
                    continue;
                }
                else
                {
                    _logger.LogWarning("Concurrency exception after retries. Attempt = {attempt}", attempt);
                }
            }

            return result;
        }
    }

    private Task BackoffDelay(int attempt, CancellationToken ct)
        => Task.Delay(_retryPolicy.BackOffDelayMs(attempt), ct);
}