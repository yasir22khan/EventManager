using EventManagement.Domain.Entities;
using EventManagement.Domain.Repositories;
namespace EventManagement.App.DataSeeder;

public class DataSeeder {
    private readonly IUnitOfWork _unitOfWork;
    private readonly Persistence.ApplicationDbContext context;

    public DataSeeder(IUnitOfWork unitOfWork, Persistence.ApplicationDbContext c) {
        _unitOfWork = unitOfWork;
        context = c;
    }

    public void Seed() {
        SeedEvents();
    }
    private void SeedEvents() {
        var events = new List<Event>();
        for (int i = 1; i <= 10; i++) {            
            events.Add(new Event {                                
                Title = $"1st Event {i}",
                Description = $"Event Description {i}",
                EventStartDate = DateTime.UtcNow,
                EventEndDate = DateTime.UtcNow,
                EventLocation = "Lahore, Pakistan",
                CreatedOn = DateTime.UtcNow
            });
        }
        context.Set<Event>().AddRange(events);
        _unitOfWork.SaveChanges();
    }
}
