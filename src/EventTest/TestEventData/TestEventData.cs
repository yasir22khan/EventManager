using EventManagement.Domain.Entities;

namespace EventTest.TestEventData; 

public class TestEventData {
    public static List<Event> GetEvents()
    {
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

        return events;
    }
    public static Event NewEvent()
    {
        return new Event {                                
            Title = "1st Event",
            Description = "Event Description",
            EventStartDate = DateTime.UtcNow,
            EventEndDate = DateTime.UtcNow,
            EventLocation = "Lahore, Pakistan",
            CreatedOn = DateTime.UtcNow
        };
    }
}