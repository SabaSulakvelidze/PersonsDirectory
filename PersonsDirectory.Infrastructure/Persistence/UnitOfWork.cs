using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Infrastructure.Persistence.Repositories;

namespace PersonsDirectory.Infrastructure.Persistence
{
    public sealed class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {

        public IPersonRepository Persons { get; } = new PersonRepository(_context);
        public ICityRepository Cities { get; } = new CityRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);
    }
}
