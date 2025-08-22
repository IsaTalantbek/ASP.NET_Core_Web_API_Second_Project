namespace Application.Users.Commands.CreateUser;

public abstract record CreateUserCommandResult
{
    public record Success(Guid CreatedUserGuid, Guid CreatedAccountGuid) : CreateUserCommandResult;
}