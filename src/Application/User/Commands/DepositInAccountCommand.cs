using MediatR;

namespace Application.User.Commands;

public class DepositInAccountCommand : IRequest
{
    public Guid AccountId { get; init; }
    public long Amount { get; init; }

    public DepositInAccountCommand(Guid accountId, long amount)
    {
        AccountId = accountId;
        Amount = amount;
    }
}
