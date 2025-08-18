using DomainUser = Domain.Users.Aggregates.User.User;

namespace Application.User.Repositories;

public interface IUserRepository
{
    Task<DomainUser?> GetByIdAsync(Guid id);
    Task Create(DomainUser user);
}