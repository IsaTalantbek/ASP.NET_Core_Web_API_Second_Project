using Application.User.DTOs;
using Application.User.Repositories;
using MediatR;

namespace Application.User.Queries;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO?>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDTO?> Handle(GetAccountByIdQuery query, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(query.AccountId);

        if (account == null)
            return null;

        return new AccountDTO(account.Id, account.UserId, account.Balance);
    }
}

