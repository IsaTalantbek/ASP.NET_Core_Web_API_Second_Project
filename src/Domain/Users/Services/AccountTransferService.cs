using Domain.Users.Aggregates.Account;

namespace Domain.Users.Services;

public class AccountTransferService
{
    public void Transfer(Account from, Account to, int amount)
    {
        from.Debit(amount);
        to.Deposit(amount);
    }
}