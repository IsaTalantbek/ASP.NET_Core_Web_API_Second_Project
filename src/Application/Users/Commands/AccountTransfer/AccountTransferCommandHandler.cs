using Application.Services.Retry;
using Application.System.UnitOfWork;
using Application.Users.DTOs;
using Application.Users.Repositories;
using AutoMapper;
using Domain.Users.Services;
using MediatR;

namespace Application.Users.Commands.UserTransfer;

public class AccountTransferCommandHandler : IRequestHandler<AccountTransferCommand, AccountTransferCommandResult>
{
    private readonly IUnitOfWork _uow;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly RetryService _retryService;
    private readonly AccountTransferService _accountTransferService;

    public AccountTransferCommandHandler(IUnitOfWork uow,
        AccountTransferService accountTransferService,
        RetryService retryService,
        IMapper mapper,
        IAccountRepository accountRepository)
    {
        _uow = uow;
        _accountTransferService = accountTransferService;
        _retryService = retryService;
        _mapper = mapper;
        _accountRepository = accountRepository;
    }

    public async Task<AccountTransferCommandResult> Handle(AccountTransferCommand command, CancellationToken ct)
    {
        if (command.ToAccountId == command.FromAccountId)
            return new AccountTransferCommandResult.SelfTransaction(command.Amount);

        if (command.Amount < 0)
            return new AccountTransferCommandResult.NegativeAmount(command.Amount);

        return await _retryService.ExecuteAsync<AccountTransferCommandResult>(async () =>
        {
            var from = await _accountRepository.GetByIdAsync(command.FromAccountId);

            if (from == null)
                return new AccountTransferCommandResult.NotFound(command.FromAccountId);

            var to = await _accountRepository.GetByIdAsync(command.ToAccountId, ct);

            if (to == null)
                return new AccountTransferCommandResult.NotFound(command.ToAccountId);

            if (from.BalanceAmount - command.Amount < 0)
                return new AccountTransferCommandResult.NegativeBalance(_mapper.Map<AccountDTO>(from), command.Amount);

            _accountTransferService.Transfer(from, to, command.Amount);

            var result = await _uow.SaveChangesAsync(ct);

            return result switch
            {
                UnitOfWorkResult.ConcurrencyException => new AccountTransferCommandResult.ConcurrencyException(),
                UnitOfWorkResult.Success => new AccountTransferCommandResult.Success(
                    _mapper.Map<AccountDTO>(to),
                    _mapper.Map<AccountDTO>(from),
                    command.Amount),
                _ => throw new InvalidOperationException("Invalid result from unit of work")
            };
        },
        (AccountTransferCommandResult r) => r is AccountTransferCommandResult.ConcurrencyException,
        ct);

    }
}