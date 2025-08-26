using Application.Services.Retry;
using Application.System.UnitOfWork;
using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Users.Commands.DepositInAccount;

public class DepositInAccountCommandHandler : IRequestHandler<DepositInAccountCommand, DepositInAccountCommandResult>
{
    private readonly IUnitOfWork _uow;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly RetryService _retryService;

    public DepositInAccountCommandHandler(IUnitOfWork uow, IAccountRepository accountRepository, IMapper mapper, RetryService retryService)
    {
        _uow = uow;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _retryService = retryService;
    }   

    public async Task<DepositInAccountCommandResult> Handle(DepositInAccountCommand command, CancellationToken ct)
    {
        if (command.Amount < 0)
            return new DepositInAccountCommandResult.NegativeAmount(command.Amount);

        return await _retryService.ExecuteAsync<DepositInAccountCommandResult>(async () =>
        {

            var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);

            if (account == null)
                return new DepositInAccountCommandResult.NotFound(command.AccountId);

            account.Deposit(command.Amount);

            var result = await _uow.SaveChangesAsync(ct);

            return result switch
            {
                UnitOfWorkResult.Success => new DepositInAccountCommandResult.Success(_mapper.Map<AccountDTO>(account)),
                UnitOfWorkResult.ConcurrencyException => new DepositInAccountCommandResult.ConcurrencyException(),
                _ => throw new InvalidOperationException("Invalid result from unit of work")
            };
        },
        (DepositInAccountCommandResult r) => r is DepositInAccountCommandResult.ConcurrencyException,
        ct);
    }
}