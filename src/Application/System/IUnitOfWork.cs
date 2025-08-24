namespace Application.System;

public interface IUnitOfWork
{
    Task<UnitOfWorkResult> SaveChangesAsync(CancellationToken ct = default);
}