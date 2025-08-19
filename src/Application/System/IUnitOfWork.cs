namespace Application.System;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
