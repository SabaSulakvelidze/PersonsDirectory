
using PersonsDirectory.Application.Common.Interfaces;
using PersonsDirectory.Domain.Entities;

namespace PersonsDirectory.Infrastructure.Persistence.Repositories
{
    public sealed class CityRepository(AppDbContext _context): GenericRepository<City>(_context), ICityRepository 
    {
    }
}
