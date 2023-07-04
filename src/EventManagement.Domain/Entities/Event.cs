namespace EventManagement.Domain.Entities;

public sealed class Event {
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