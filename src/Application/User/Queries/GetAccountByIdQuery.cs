using Application.User.DTOs;
using MediatR;

namespace Application.User.Queries;

public class GetAccountByIdQuery : IRequest<AccountDTO?>
{
    public Guid AccountId { get; init; }

    public GetAccountByIdQuery(Guid accountId)
    {
        AccountId = accountId;
    }
}