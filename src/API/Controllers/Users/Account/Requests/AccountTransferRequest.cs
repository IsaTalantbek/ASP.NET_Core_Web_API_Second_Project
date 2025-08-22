using System.Text.Json.Serialization;

namespace API.Controllers.Users.Account.Requests;

public class AccountTransferRequest
{
    public Guid From { get; init; }
    public Guid To { get; init; }
    public int Amount { get; init; }

    public AccountTransferRequest(Guid from, Guid to, int amount)
    {
        From = from;
        To = to;
        Amount = amount;
    }
}