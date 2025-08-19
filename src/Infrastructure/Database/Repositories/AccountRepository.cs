using Application.User.Repositories;
using Domain.Users.Aggregates.Account;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ProjectDbContext _context;

    public AccountRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid accountId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public async Task Create(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }
}