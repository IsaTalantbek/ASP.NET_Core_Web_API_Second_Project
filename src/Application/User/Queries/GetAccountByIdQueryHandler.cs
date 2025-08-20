using Application.User.DTOs;
using Application.User.Repositories;
using AutoMapper;
using MediatR;

namespace Application.User.Queries;

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
        var account = await _accountRepository.GetByIdAsync(query.AccountId);

        if (account == null)
            return null;

        return _mapper.Map<AccountDTO>(account);
    }
}

