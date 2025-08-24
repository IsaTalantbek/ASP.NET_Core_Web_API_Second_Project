using Application.System;
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

    public DepositInAccountCommandHandler(IUnitOfWork uow, IAccountRepository accountRepository, IMapper mapper)
    {
        _uow = uow;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public async Task<DepositInAccountCommandResult> Handle(DepositInAccountCommand command, CancellationToken ct)
    {
        if (command.Amount < 0)
            return new DepositInAccountCommandResult.NegativeAmount(command.Amount);

        var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);

        if (account == null)
            return new DepositInAccountCommandResult.NotFound(command.AccountId);

        account.Deposit(command.Amount);

        var result = await _uow.SaveChangesAsync(ct);

        if (result == UnitOfWorkResult.ConcurrencyException)
            return new DepositInAccountCommandResult.ConcurrencyException();

        return new DepositInAccountCommandResult.Success(_mapper.Map<AccountDTO>(account));
    }
}