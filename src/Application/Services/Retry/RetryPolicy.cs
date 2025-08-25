using System;

namespace Application.Services.Retry;

public class RetryPolicy
{
    private readonly int _maxAttempts;
    private readonly int _baseDelayMs;

    private readonly Random _random = new();

    public RetryPolicy(int maxAttempts, int baseDelayMs)
    {
        _maxAttempts = maxAttempts;
        _baseDelayMs = baseDelayMs;
    }

    public bool ShouldNextAttempt(int attempt)
        => attempt < _maxAttempts;

    public int BackOffDelayMs(int attempt)
        => _baseDelayMs * attempt + _random.Next(20, 100);
} 