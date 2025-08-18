using DomainAccount = Domain.Users.Aggregates.Account.Account;

namespace Application.User.Repositories;

public interface IAccountRepository
{
    Task<DomainAccount?> GetByIdAsync(Guid id);
    Task Create(DomainAccount user);
}