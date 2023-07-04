using Microsoft.EntityFrameworkCore;
using EventManagement.Domain.Repositories;
using System.Linq.Expressions;

namespace EventManagement.Persistence.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class {
    protected ApplicationDbContext context;
    internal DbSet<T> dbSet;

    public GenericRepository(ApplicationDbContext context) {
        context = context;
        dbSet = context.Set<T>();
    }

    public virtual async Task<T> GetById(Guid id) => await dbSet.FindAsync(id);

    public virtual async Task<bool> Add(T entity) {
        await dbSet.AddAsync(entity);
        return true;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) {
        await dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task<bool> Delete(Guid id) {
        throw new NotImplementedException();
    }

    public virtual Task<IEnumerable<T>> All() {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate) {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public virtual Task<bool> Upsert(T entity) {
        throw new NotImplementedException();
    }
}
