
using EventManagement.Application.Models;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Presentation.Controllers.v1 {
    [Route("api/events")]
    public sealed class EventsController : ControllerBase {
        private readonly IEventRepository _EventRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EventsController(IEventRepository EventRepository, IUnitOfWork unitOfWork) {
            _EventRepository = EventRepository;
             _unitOfWork = unitOfWork;
        }
        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventModel eventDetail, CancellationToken cancellationToken) {

            var newEvent = new Event {
                Title = eventDetail.Title,
                Description = eventDetail.Description,
                EventStartDate = eventDetail.EventStartDate,
                EventEndDate = eventDetail.EventEndDate,
                EventLocation = eventDetail.EventLocation,
                CreatedOn = DateTime.Now,
                CreatedBy = new Guid()
            };
            await _EventRepository.AddAsync(newEvent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }

        [HttpPatch("{eventId:guid}")]
        public async Task<IActionResult> Update(Guid eventId, [FromBody] EventModel eventDetail, CancellationToken cancellationToken) {            
            var existingEvent = await _EventRepository.GetByIdAsync(eventId, cancellationToken);
            if(existingEvent != null) {
                existingEvent.Title = eventDetail.Title;
                existingEvent.Description = eventDetail.Description;    
                existingEvent.EventStartDate = eventDetail.EventStartDate;
                existingEvent.EventEndDate = eventDetail.EventEndDate;
                existingEvent.EventLocation = eventDetail.EventLocation;
                existingEvent.ModifiedOn = eventDetail.ModifiedOn;
                existingEvent.ModifiedBy = eventDetail.ModifiedBy;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Ok(existingEvent);
        }
        

        [HttpGet()]
        public async Task<IActionResult> Get(CancellationToken cancellationToken) {
            var events = await _EventRepository.GetEventsAsync(cancellationToken);
            return Ok(events);
        }

        [HttpGet("{eventId:guid}")]
        public async Task<IActionResult> GetById(Guid eventId, CancellationToken cancellationToken) {
            var eventRecord = await _EventRepository.GetByIdAsync(eventId, cancellationToken);
            return Ok(eventRecord);
        }        
    }
}