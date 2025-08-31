using FlightApi.Models;
using FlightApi.Repositories;

namespace FlightApi.Services
{
    /// <summary>
    /// Service for handling business logic related to flights.
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightService"/> class.
        /// </summary>
        /// <param name="repo">Flight repository.</param>
        public FlightService(IFlightRepository repo) => _repo = repo;

        /// <inheritdoc/>
        public IEnumerable<Flight> GetAll() => _repo.GetAll();

        /// <inheritdoc/>
        public Flight GetById(int id) => _repo.GetById(id);

        /// <inheritdoc/>
        public Flight Create(Flight flight) => _repo.Create(flight);

        /// <inheritdoc/>
        public void Update(int id, Flight flight) => _repo.Update(id, flight);

        /// <inheritdoc/>
        public void Delete(int id) => _repo.Delete(id);

        /// <inheritdoc/>
        public IEnumerable<Flight> Search(string airline, string departure, string arrival) => _repo.Search(airline, departure, arrival);
    }
}