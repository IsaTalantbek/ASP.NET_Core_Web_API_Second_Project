using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountDTO?>
{
    public Guid AccountId { get; init; }

    public GetAccountByIdQuery(Guid accountId)
    {
        AccountId = accountId;
    }
}