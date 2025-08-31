using FlightApi.Models;

namespace FlightApi.Services
{
    /// <summary>
    /// Interface for flight service operations.
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Gets all flights.
        /// </summary>
        IEnumerable<Flight> GetAll();

        /// <summary>
        /// Gets a flight by ID.
        /// </summary>
        /// <param name="id">Flight ID.</param>
        Flight GetById(int id);

        /// <summary>
        /// Creates a new flight.
        /// </summary>
        /// <param name="flight">Flight to create.</param>
        Flight Create(Flight flight);

        /// <summary>
        /// Updates an existing flight.
        /// </summary>
        /// <param name="id">Flight ID.</param>
        /// <param name="flight">Updated flight data.</param>
        void Update(int id, Flight flight);

        /// <summary>
        /// Deletes a flight by ID.
        /// </summary>
        /// <param name="id">Flight ID.</param>
        void Delete(int id);

        /// <summary>
        /// Searches for flights by airline, departure, or arrival.
        /// </summary>
        /// <param name="airline">Airline name.</param>
        /// <param name="departure">Departure airport.</param>
        /// <param name="arrival">Arrival airport.</param>
        IEnumerable<Flight> Search(string airline, string departure, string arrival);
    }
}