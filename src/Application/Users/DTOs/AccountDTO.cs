namespace Application.Users.DTOs;

public class AccountDTO
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public long BalanceAmount { get; init; }

    public AccountDTO(Guid id, Guid userId, long balanceAmount)
    {
        Id = id;
        UserId = userId;
        BalanceAmount = balanceAmount;
    }
}