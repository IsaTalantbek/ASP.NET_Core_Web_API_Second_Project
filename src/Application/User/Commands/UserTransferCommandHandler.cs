using Application.System;
using Application.User.Repositories;
using Domain.Users.Services;
using MediatR;

namespace Application.User.Commands;

public class UserTransferCommandHandler : IRequestHandler<UserTransferCommand>
{
    private readonly IUnitOfWork _uow;
    private readonly AccountTransferService _accountTransferService;
    private readonly IAccountRepository _accountRepository;

    public UserTransferCommandHandler(IUnitOfWork uow,
        AccountTransferService accountTransferService,
        IAccountRepository accountRepository)
    {
        _uow = uow;
        _accountTransferService = accountTransferService;
        _accountRepository = accountRepository;
    }

    public async Task Handle(UserTransferCommand command, CancellationToken ct)
    {
        var from = await _accountRepository.GetByIdAsync(command.FromAccountId);

        if (from == null)
            throw new InvalidOperationException($"Аккаунта с айди {command.FromAccountId} не существует");

        var to = await _accountRepository.GetByIdAsync(command.ToAccountId);

        if (to == null)
            throw new InvalidOperationException($"Аккаунта с айди {command.ToAccountId} не существует");

        _accountTransferService.Transfer(from, to, command.Amount);

        await _uow.SaveChangesAsync(ct);
    }
}