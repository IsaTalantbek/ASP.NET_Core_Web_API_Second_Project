using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDTO>>
{
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsQueryHandler(IMapper mapper, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    public async Task<List<AccountDTO>> Handle(GetAllAccountsQuery _, CancellationToken ct)
    {
        var accounts = await _accountRepository.GetAllAsync(isReadonly: true, ct);
        return _mapper.Map<List<AccountDTO>>(accounts);
    }
}