using System.Linq.Expressions;

namespace EventManagement.Domain.Repositories;

public interface IGenericRepository<T> where T : class {
    Task<IEnumerable<T>> All();
    Task<T> GetById(Guid id);
    Task<bool> Add(T entity);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<bool> Delete(Guid id);
    Task<bool> Upsert(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}
