namespace Application.System.UnitOfWork;

public interface IUnitOfWork
{
    Task<UnitOfWorkResult> SaveChangesAsync(CancellationToken ct = default);
}