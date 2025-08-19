using Application.User.Repositories;
using Domain.Users.Aggregates.Account;
using Domain.Users.Aggregates.User;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ProjectDbContext _context;

    public UserRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }
}