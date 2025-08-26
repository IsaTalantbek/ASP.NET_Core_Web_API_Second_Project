namespace Application.System.UnitOfWork;

public enum UnitOfWorkResult
{
    Success,
    ConcurrencyException
}