using Application.System;
using Application.User.Repositories;
using MediatR;

namespace Application.User.Commands;

public class DepositInAccountCommandHandler : IRequestHandler<DepositInAccountCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly IAccountRepository _accountRepository;

    public DepositInAccountCommandHandler(IUnitOfWork uow, IAccountRepository accountRepository)
    {
        _uow = uow;
        _accountRepository = accountRepository;
    }

    public async Task Handle(DepositInAccountCommand command, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);

        if (account == null)
            throw new InvalidOperationException($"Аккаунта с айди {command.AccountId} не существует");

        account.Deposit(command.Amount);

        await _uow.SaveChangesAsync(ct);
    }
}