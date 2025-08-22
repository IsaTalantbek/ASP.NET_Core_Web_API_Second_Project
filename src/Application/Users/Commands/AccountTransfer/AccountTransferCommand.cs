using MediatR;

namespace Application.Users.Commands.UserTransfer;

public class AccountTransferCommand : IRequest<AccountTransferCommandResult>
{
    public Guid FromAccountId { get; init; }
    public Guid ToAccountId { get; init; }
    public long Amount { get; init; }

    public AccountTransferCommand(Guid fromAccountId, Guid toAccountId, long amount)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
}