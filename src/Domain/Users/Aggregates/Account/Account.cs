namespace Domain.Users.Aggregates.Account;

public class Account
{
    public Guid UserId { get; init; }
    public int Balance { get; private set; } = 0;

    public Account(Guid userId)
    {
        UserId = userId;
    }

    public void Debit(int amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Нельзя чтобы аргумент был отрицательным: {amount}");

        var newBalance = Balance - amount;

        if (newBalance < 0)
            throw new InvalidOperationException($"Баланс стал отрицательным: {newBalance}, Полученное значение: {amount}");

        Balance = newBalance;
    }

    public void Deposit(int amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Нельзя чтобы значение было минусовым: {amount}");

        Balance += amount;
    }
}