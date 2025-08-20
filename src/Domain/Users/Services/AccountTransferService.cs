using Domain.Users.Aggregates;

namespace Domain.Users.Services;

public class AccountTransferService
{
    public void Transfer(Account from, Account to, long amount)
    {
        from.Debit(amount);
        to.Deposit(amount);
    }
}