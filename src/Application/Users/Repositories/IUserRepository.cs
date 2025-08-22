using DomainUser = Domain.Users.Aggregates.User;

namespace Application.Users.Repositories;

public interface IUserRepository
{
    Task<List<DomainUser>> GetAllAsync(CancellationToken ct = default);
    Task<DomainUser?> GetByIdAsync(Guid userId, CancellationToken ct = default);
    Task Create(DomainUser user, CancellationToken ct = default);
}