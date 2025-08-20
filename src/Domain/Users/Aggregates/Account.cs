/*
    Domain/ - слой
    Users/ - Bounded Context
    Aggregates/ - Aggregates
    Account/ - Aggregate
    Account.cs - Root Aggregate (Через него идет взаимодействие с связанными объектами и свойствами в агрегате Account. Их пока что нет)
*/

namespace Domain.Users.Aggregates;

public class Account
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public long Balance { get; private set; } = 0;

    // Нужно для создания через ОРМ (игнорировать)
    private Account() {}

    public Account(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    // Контролирует свои инварианты
    public void Debit(long amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Нельзя чтобы аргумент был отрицательным: {amount}");

        var newBalance = Balance - amount;

        if (newBalance < 0)
            throw new InvalidOperationException($"Баланс стал отрицательным: {newBalance}, Полученное значение: {amount}");

        Balance = newBalance;
    }

    // Контролирует свои инварианты
    public void Deposit(long amount)
    {
        if (amount < 0)
            throw new ArgumentException($"Нельзя чтобы аргумент был отрицательным: {amount}");

        Balance += amount;
    }
}