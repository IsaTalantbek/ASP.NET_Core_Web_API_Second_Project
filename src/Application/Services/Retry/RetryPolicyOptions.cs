namespace Application.Services.Retry;

public class RetryPolicyOptions
{
    public int MaxAttempts { get; set; }
    public int BaseDelayMs { get; set; }
}
