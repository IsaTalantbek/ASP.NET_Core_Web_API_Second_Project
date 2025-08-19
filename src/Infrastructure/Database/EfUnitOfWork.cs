using Application.System;

namespace Infrastructure.Database;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ProjectDbContext _context;

    public EfUnitOfWork(ProjectDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
        return;
    }
}