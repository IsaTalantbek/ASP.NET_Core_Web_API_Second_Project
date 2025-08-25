using Application.System;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ProjectDbContext _context;
    private readonly ILogger _logger;

    public EfUnitOfWork(ProjectDbContext context, ILogger<EfUnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UnitOfWorkResult> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            await _context.SaveChangesAsync(ct);
            return UnitOfWorkResult.Success;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            foreach (var entry in _context.ChangeTracker.Entries().ToList())
                entry.State = EntityState.Detached;

            return UnitOfWorkResult.ConcurrencyException;
        }
    }
}