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
    {
        return await _context.Users.ToListAsync(ct);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _context.Users.FirstOrDefaultAsync(a => a.Id == userId, ct);
    }

    public async Task Create(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
    }
}