using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetAccountById;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO?>
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public GetAccountByIdQueryHandler(IMapper mapper, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    public async Task<AccountDTO?> Handle(GetAccountByIdQuery query, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(query.AccountId, isReadonly: true, ct);

        if (account == null)
            return null;

        return _mapper.Map<AccountDTO>(account);
    }
}

