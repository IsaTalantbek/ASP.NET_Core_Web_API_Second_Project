using Application.Users.Repositories;
using Domain.Users.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ProjectDbContext _context;

    public UserRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken ct)
        => await GetAllAsync(false, ct);

    public async Task<List<User>> GetAllAsync(bool isReadonly, CancellationToken ct)
    {
        var query = isReadonly
            ? _context.Users.AsNoTracking()
            : _context.Users;

        return await query.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
        => await GetByIdAsync(userId, false, ct);

    public async Task<User?> GetByIdAsync(Guid userId, bool isReadonly, CancellationToken ct)
    {
        var query = isReadonly
            ? _context.Users.AsNoTracking()
            : _context.Users;

        return await query.FirstOrDefaultAsync(a => a.Id == userId, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
    }
}