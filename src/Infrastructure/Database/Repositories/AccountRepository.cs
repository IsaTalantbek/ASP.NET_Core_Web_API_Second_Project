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
        => await GetAllAsync(false, ct);

    public async Task<List<Account>> GetAllAsync(bool isReadonly, CancellationToken ct)
    {
        var query = isReadonly
            ? _context.Accounts.AsNoTracking()
            : _context.Accounts;

        return await query.ToListAsync(ct);
    }

    public async Task<Account?> GetByIdAsync(Guid accountId, CancellationToken ct)
        => await GetByIdAsync(accountId, false, ct);

    public async Task<Account?> GetByIdAsync(Guid accountId, bool isReadonly, CancellationToken ct)
    {
        var query = isReadonly 
            ? _context.Accounts.AsNoTracking() 
            : _context.Accounts;

        return await query.FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public async Task AddAsync(Account account, CancellationToken ct)
    {
        await _context.Accounts.AddAsync(account, ct);
    }
}