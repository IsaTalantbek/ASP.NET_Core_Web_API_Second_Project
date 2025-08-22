namespace API.Controllers.Users.Account.Requests;

public class AccountDepositRequest
{
    public long Amount { get; init; }

    public AccountDepositRequest(long amount)
    {
        Amount = amount;
    }
}