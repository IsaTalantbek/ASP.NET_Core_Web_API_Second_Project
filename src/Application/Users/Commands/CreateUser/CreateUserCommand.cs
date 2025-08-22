using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserCommandResult>
{ 
    public Guid UserId { get; init; }
    public Guid AccountId { get; init; }
    public string Name { get; init; }

    public CreateUserCommand(Guid userId, Guid accountId, string name)
    {
        UserId = userId;
        AccountId = accountId;
        Name = name;
    }
}