namespace Application.User.DTOs;

public class AccountDTO
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public long Balance { get; init; }

    public AccountDTO(Guid id, Guid userId, long balance)
    {
        Id = id;
        UserId = userId;
        Balance = balance;
    }
}