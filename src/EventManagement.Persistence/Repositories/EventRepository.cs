using Microsoft.EntityFrameworkCore;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Repositories;

namespace EventManagement.Persistence.Repositories;

public sealed class EventRepository : IEventRepository {
    private readonly ApplicationDbContext _dbContext;

    public EventRepository(ApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public Task<List<Event>> GetEventsAsync(CancellationToken cancellationToken = default) => 
        _dbContext.Set<Event>().ToListAsync(cancellationToken);
    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext
            .Set<Event>()
            .FirstOrDefaultAsync(ev => ev.Id == id, cancellationToken);

    public void Add(Event Event) => _dbContext.Set<Event>().Add(Event);
    
    public async Task AddAsync(Event Event, CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Event>().AddAsync(Event, cancellationToken);

    public void Update(Event Event) => _dbContext.Set<Event>().Update(Event);   
}
