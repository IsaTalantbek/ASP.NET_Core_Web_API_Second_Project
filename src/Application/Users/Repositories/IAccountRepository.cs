using Domain.Users.Aggregates;

namespace Application.Users.Repositories;

public interface IAccountRepository
{

    Task<List<Account>> GetAllAsync(bool isReadonly, CancellationToken ct = default);
    Task<List<Account>> GetAllAsync(CancellationToken ct = default);
    Task<Account?> GetByIdAsync(Guid id, bool isReadonly, CancellationToken ct = default);
    Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Account account, CancellationToken ct = default);
}