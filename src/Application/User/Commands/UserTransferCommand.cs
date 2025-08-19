using MediatR;

namespace Application.User.Commands;

public class UserTransferCommand : IRequest
{
    public Guid FromAccountId { get; init; }
    public Guid ToAccountId { get; init; }
    public long Amount { get; init; }

    public UserTransferCommand(Guid fromAccountId, Guid toAccountId, long amount)
    {
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
    }
}