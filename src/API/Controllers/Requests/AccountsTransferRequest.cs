using System.Text.Json.Serialization;

namespace API.Controllers.Requests;

public class AccountsTransferRequest
{
    public Guid From { get; init; }
    public Guid To { get; init; }
    public int Amount { get; init; }

    [JsonConstructor]
    public AccountsTransferRequest(Guid from, Guid to, int amount)
    {
        From = from;
        To = to;
        Amount = amount;
    }
}