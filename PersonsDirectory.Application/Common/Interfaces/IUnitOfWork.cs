namespace PersonsDirectory.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; }
        ICityRepository Cities { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
