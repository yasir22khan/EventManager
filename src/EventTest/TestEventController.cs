using EventManagement.Domain.Repositories;
using EventManagement.Presentation.Controllers.v1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventTest; 

public class TestEventController {
    [Fact]
    public async Task GetAllAsync_ShouldReturn200Status()
    {
        /// Arrange
        var eventRepo = new Mock<IEventRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        eventRepo.Setup(_ => _.GetEventsAsync(CancellationToken.None))
            .ReturnsAsync(TestEventData.TestEventData.GetEvents());
        var sut = new EventsController(eventRepo.Object, unitOfWork.Object);

        /// Act
        var result = (OkObjectResult)await sut.Get(CancellationToken.None);


        // /// Assert
        result.StatusCode.Should().Be(200);
    }

    
    }