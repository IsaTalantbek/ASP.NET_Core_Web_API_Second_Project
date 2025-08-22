using Application.Users.DTOs;

namespace Application.Users.Commands.DepositInAccount;

public abstract record DepositInAccountCommandResult
{
    public record Success(AccountDTO Account) : DepositInAccountCommandResult;

    public record NotFound(Guid AccountId) : DepositInAccountCommandResult;

    public record NegativeAmount(long Amount) : DepositInAccountCommandResult;
} 