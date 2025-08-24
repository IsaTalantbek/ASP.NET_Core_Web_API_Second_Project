using Application.System;
using Application.Users.Repositories;
using Domain.Users.Services;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResult>
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

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand command, CancellationToken ct)
    {
        var (user, account) = _createUserService.Create(command.UserId, command.AccountId, command.Name);

        await _userRepository.AddAsync(user, ct);
        await _accountRepository.AddAsync(account, ct);

        await _uow.SaveChangesAsync(ct);

        return new CreateUserCommandResult.Success(user.Id, account.Id);
    }
}