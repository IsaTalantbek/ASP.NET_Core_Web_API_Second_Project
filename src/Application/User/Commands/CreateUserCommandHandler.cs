using Application.System;
using Application.User.Repositories;
using Domain.Users.Services;
using MediatR;

namespace Application.User.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly CreateUserService _createUserService;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public CreateUserCommandHandler(IUnitOfWork uow,
        CreateUserService createUserService,
        IUserRepository userRepository,
        IAccountRepository accountRepository)
    {
        _uow = uow;
        _createUserService = createUserService;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var (user, account) = _createUserService.Create(command.UserId, command.AccountId, command.Name);

        await _userRepository.Create(user);
        await _accountRepository.Create(account);

        await _uow.SaveChangesAsync(ct);

        return command.UserId;
    }
}