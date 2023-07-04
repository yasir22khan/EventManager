using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Repositories;

public interface IEventRepository {
    Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Event>> GetEventsAsync(CancellationToken cancellationToken = default);     
    
    void Add(Event Event);
    Task AddAsync(Event Event, CancellationToken cancellationToken = default);
    void Update(Event Event);
}
