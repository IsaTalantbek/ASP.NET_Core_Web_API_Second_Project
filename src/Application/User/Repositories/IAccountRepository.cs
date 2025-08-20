using Domain.Users.Aggregates;

namespace Application.User.Repositories;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAsync(CancellationToken ct = default);
    Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task Create(Account account, CancellationToken ct = default);
}