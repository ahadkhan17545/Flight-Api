using FlightApi.Models;
using FlightApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightApi.Controllers
{
    /// <summary>
    /// API controller for managing flight resources. Supports CRUD operations and search functionality.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ILogger<FlightsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightsController"/> class.
        /// </summary>
        /// <param name="flightService">Service for handling flight data operations.</param>
        /// <param name="logger">Logger for controller actions.</param>
        public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all flights.
        /// </summary>
        /// <returns>HTTP 200 with a list of all flights.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var flights = _flightService.GetAll();
            return Ok(flights);
        }

        /// <summary>
        /// Retrieves a specific flight by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the flight.</param>
        /// <returns>HTTP 200 with the flight if found; otherwise, HTTP 404.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flight = _flightService.GetById(id);
            if (flight == null)
            {
                _logger.LogWarning("Flight with ID {Id} not found (TraceId: {TraceId})", id, HttpContext.TraceIdentifier);
                return NotFound(new { Message = $"Flight with ID {id} not found.", TraceId = HttpContext.TraceIdentifier });
            }

            return Ok(flight);
        }

        /// <summary>
        /// Creates a new flight.
        /// </summary>
        /// <param name="flight">The flight details to create.</param>
        /// <returns>HTTP 201 with the created flight; HTTP 400 if the model is invalid.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid flight model submitted: {@ModelState} (TraceId: {TraceId})", ModelState, HttpContext.TraceIdentifier);
                return BadRequest(new { Errors = ModelState, TraceId = HttpContext.TraceIdentifier });
            }

            var created = _flightService.Create(flight);
            _logger.LogInformation("Flight with ID {Id} created successfully (TraceId: {TraceId})", created.Id, HttpContext.TraceIdentifier);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing flight.
        /// </summary>
        /// <param name="id">The unique identifier of the flight to update.</param>
        /// <param name="flight">The updated flight data.</param>
        /// <returns>HTTP 204 if successful; HTTP 404 if not found; HTTP 400 if model is invalid.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Flight flight)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid flight model for update: {@ModelState} (TraceId: {TraceId})", ModelState, HttpContext.TraceIdentifier);
                return BadRequest(new { Errors = ModelState, TraceId = HttpContext.TraceIdentifier });
            }

            var existing = _flightService.GetById(id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed. Flight with ID {Id} not found (TraceId: {TraceId})", id, HttpContext.TraceIdentifier);
                return NotFound(new ProblemDetails
                {
                    Title = "Flight not found",
                    Detail = $"Flight with ID {id} not found.",
                    Status = 404,
                    Instance = HttpContext.TraceIdentifier
                });
            }

            _flightService.Update(id, flight);
            _logger.LogInformation("Flight with ID {Id} updated successfully (TraceId: {TraceId})", id, HttpContext.TraceIdentifier);

            return NoContent();
        }

        /// <summary>
        /// Deletes a flight by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the flight to delete.</param>
        /// <returns>HTTP 204 if successful; HTTP 404 if not found.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var flight = _flightService.GetById(id);
            if (flight == null)
            {
                _logger.LogWarning("Delete failed. Flight with ID {Id} not found (TraceId: {TraceId})", id, HttpContext.TraceIdentifier);
                return NotFound(new { Message = $"Flight with ID {id} not found.", TraceId = HttpContext.TraceIdentifier });
            }

            _flightService.Delete(id);
            _logger.LogInformation("Flight with ID {Id} deleted successfully (TraceId: {TraceId})", id, HttpContext.TraceIdentifier);

            return NoContent();
        }

        /// <summary>
        /// Searches for flights by airline, departure airport, or arrival airport.
        /// </summary>
        /// <param name="airline">Optional airline name to filter by.</param>
        /// <param name="departure">Optional departure airport to filter by.</param>
        /// <param name="arrival">Optional arrival airport to filter by.</param>
        /// <returns>HTTP 200 with a list of flights matching the search criteria.</returns>
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? airline, [FromQuery] string? departure, [FromQuery] string? arrival)
        {
            var airlineValue = airline ?? string.Empty;
            var departureValue = departure ?? string.Empty;
            var arrivalValue = arrival ?? string.Empty;

            var results = _flightService.Search(airlineValue, departureValue, arrivalValue);
            return Ok(results);
        }
    }
}
