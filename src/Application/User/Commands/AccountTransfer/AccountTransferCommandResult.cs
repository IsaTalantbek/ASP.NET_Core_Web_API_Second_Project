using Application.User.DTOs;

public abstract record AccountTransferCommandResult
{
    public record Success(AccountDTO To, AccountDTO From, long Amount) : AccountTransferCommandResult;

    public record NotFound(Guid AccountId) : AccountTransferCommandResult;

    public record NegativeAmount(long Amount) : AccountTransferCommandResult;

    public record NegativeBalance(AccountDTO NegativeBalanceAccount, long Amount) : AccountTransferCommandResult;
}