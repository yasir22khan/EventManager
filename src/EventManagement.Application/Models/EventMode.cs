namespace EventManagement.Application.Models;

public class EventModel {
    public Guid Id { get; set;}
    public string Title { get; set; }
    public string Description { get; set; }    
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    public string EventLocation { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }    
    public DateTime? ModifiedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
}