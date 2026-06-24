
using Microsoft.EntityFrameworkCore;
using PersonsDirectory.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace PersonsDirectory.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> Set = context.Set<T>();

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await Set.AddAsync(entity, ct);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return Set.AnyAsync(predicate, ct);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await Set.FindAsync([id], ct);
        }

        public IQueryable<T> Query()
        {
            return Set.AsNoTracking();
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }
    }
}
