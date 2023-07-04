namespace EventManagement.Domain.Repositories;
public interface IUnitOfWork {
    void SaveChanges(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
