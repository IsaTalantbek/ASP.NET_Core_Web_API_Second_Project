using Domain.Users.Aggregates.Account;
using Domain.Users.Aggregates.User;
using Domain.Users.ValueObjects;

namespace Domain.Users.Services;

public class CreateUserService
{
    public (User, Account) Create(Guid UserId, Guid AccountId, string userName)
    {
        return (
            new User(UserId, AccountId, userName),
            new Account(AccountId, UserId)
            );
    }
}
