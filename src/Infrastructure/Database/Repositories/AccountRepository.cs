using Application.Users.Repositories;
using Domain.Users.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ProjectDbContext _context;

    public AccountRepository(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task<List<Account>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Accounts.ToListAsync(ct);
    }

    public async Task<Account?> GetByIdAsync(Guid accountId, CancellationToken ct)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId, ct);
    }

    public async Task Create(Account account, CancellationToken ct)
    {
        await _context.Accounts.AddAsync(account, ct);
    }
}