using FlightApi.Controllers;
using FlightApi.Models;
using FlightApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlightApi.Tests
{
    // Unit tests for FlightsController
    public class FlightsControllerTests
    {
        // Mocked dependencies
        private readonly Mock<IFlightService> _mockService;
        private readonly Mock<ILogger<FlightsController>> _mockLogger;
        private readonly FlightsController _controller;

        // Set up controller and mocks
        public FlightsControllerTests()
        {
            _mockService = new Mock<IFlightService>();
            _mockLogger = new Mock<ILogger<FlightsController>>();
            _controller = new FlightsController(_mockService.Object, _mockLogger.Object);

            // Add this to prevent NullReferenceException in CreatedAtAction
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        // Test: GetAll returns Ok with a list of flights
        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfFlights()
        {
            var flights = new List<Flight> { new Flight { Id = 1, FlightNumber = "XY123" } };
            _mockService.Setup(s => s.GetAll()).Returns(flights);

            var result = _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnFlights = Assert.IsAssignableFrom<IEnumerable<Flight>>(okResult.Value);
            Assert.Single(returnFlights);
        }

        // Test: GetById returns Ok for an existing flight
        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            var flight = new Flight { Id = 1, FlightNumber = "XY123" };
            _mockService.Setup(s => s.GetById(1)).Returns(flight);

            var result = _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(flight, okResult.Value);
        }

        // Test: Create returns CreatedAtAction for a valid flight
        [Fact]
        public void Create_ValidFlight_ReturnsCreatedAtAction()
        {
            var flight = new Flight { Id = 1, FlightNumber = "XY123" };
            _mockService.Setup(s => s.Create(It.IsAny<Flight>())).Returns(flight);

            var result = _controller.Create(flight);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetById", createdResult.ActionName);
        }

        // Test: Update returns NoContent for a valid flight
        [Fact]
        public void Update_ValidFlight_ReturnsNoContent()
        {
            var flight = new Flight { Id = 1, FlightNumber = "XY123" };

            // Mock the GetById to simulate an existing flight
            _mockService.Setup(s => s.GetById(1)).Returns(flight);

            // Act
            var result = _controller.Update(1, flight);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // Test: Update returns NotFound when flight does not exist
        [Fact]
        public void Update_InvalidFlight_ReturnsNotFound()
        {
            // Arrange: Setup mock to return null for non-existent flight
            var flight = new Flight { Id = 999, FlightNumber = "INVALID" };
            _mockService.Setup(s => s.GetById(999)).Returns((Flight?)null!);

            // Act: Attempt to update a flight that doesn't exist
            var result = _controller.Update(999, flight);

            // Assert: Should return NotFoundObjectResult with ProblemDetails
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var problem = Assert.IsType<ProblemDetails>(notFoundResult.Value);
            Assert.Equal("Flight with ID 999 not found.", problem.Detail);
        }

        // Test: Delete returns NoContent for a valid flight ID
        [Fact]
        public void Delete_ValidId_ReturnsNoContent()
        {
            var flight = new Flight { Id = 1, FlightNumber = "XY123" };

            // Mocking GetById so the controller thinks the flight exists
            _mockService.Setup(s => s.GetById(1)).Returns(flight);

            var result = _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        // Test: Delete returns NotFound for an invalid flight ID
        [Fact]
        public void Delete_InvalidId_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetById(999)).Returns((Flight?)null!);

            var result = _controller.Delete(999);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            // Ensure notFoundResult.Value is not null before accessing its properties
            Assert.NotNull(notFoundResult.Value);

            // Convert anonymous object to dictionary
            var valueDict = notFoundResult.Value
                .GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(notFoundResult.Value));

            Assert.Equal("Flight with ID 999 not found.", valueDict["Message"]);
        }


        // Test: Search returns Ok with flights filtered by airline
        [Fact]
        public void Search_ByAirline_ReturnsOkResult()
        {
            var flights = new List<Flight> { new Flight { Id = 1, Airline = "Delta" } };
            _mockService.Setup(s => s.Search("Delta", It.IsAny<string>(), It.IsAny<string>())).Returns(flights);

            var result = _controller.Search("Delta", null, null);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Flight>>(okResult.Value);
        }
    }
}
