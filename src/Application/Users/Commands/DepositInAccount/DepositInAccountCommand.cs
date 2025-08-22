using MediatR;

namespace Application.Users.Commands.DepositInAccount;

public class DepositInAccountCommand : IRequest<DepositInAccountCommandResult>
{
    public Guid AccountId { get; init; }
    public long Amount { get; init; }

    public DepositInAccountCommand(Guid accountId, long amount)
    {
        AccountId = accountId;
        Amount = amount;
    }
}
