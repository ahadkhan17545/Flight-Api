using FlightApi.Data;
using FlightApi.Models;

namespace FlightApi.Repositories
{
    /// <summary>
    /// Repository for managing flight data in the database.
    /// </summary>
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightRepository"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public FlightRepository(FlightContext context) => _context = context;

        /// <inheritdoc/>
        public IEnumerable<Flight> GetAll() => _context.Flights.ToList();

        /// <inheritdoc/>
        public Flight GetById(int id) => _context.Flights.Find(id) ?? throw new InvalidOperationException($"Flight with ID {id} not found.");

        /// <inheritdoc/>
        public Flight Create(Flight flight) { _context.Flights.Add(flight); _context.SaveChanges(); return flight; }

        /// <inheritdoc/>
        public void Update(int id, Flight flight) { _context.Flights.Update(flight); _context.SaveChanges(); }

        /// <inheritdoc/>
        public void Delete(int id) { var f = _context.Flights.Find(id); if (f != null) { _context.Flights.Remove(f); _context.SaveChanges(); } }

        /// <inheritdoc/>
        public IEnumerable<Flight> Search(string airline, string departure, string arrival) =>
    _context.Flights.Where(f =>
        (string.IsNullOrEmpty(airline) || (f.Airline != null && f.Airline.Contains(airline))) &&
        (string.IsNullOrEmpty(departure) || (f.DepartureAirport != null && f.DepartureAirport.Contains(departure))) &&
        (string.IsNullOrEmpty(arrival) || (f.ArrivalAirport != null && f.ArrivalAirport.Contains(arrival)))
    ).ToList();
    }
}