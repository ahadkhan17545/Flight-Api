using FlightApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace FlightApi.Models
{
    /// <summary>
    /// Represents a flight with details such as flight number, airline, airports, times, and status.
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Gets or sets the unique identifier for the flight.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        [Required(ErrorMessage = "FlightNumber is required.")]
        [StringLength(10, ErrorMessage = "FlightNumber cannot exceed 10 characters.")]
        public string FlightNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the airline operating the flight.
        /// </summary>
        [Required(ErrorMessage = "Airline is required.")]
        [StringLength(50, ErrorMessage = "Airline cannot exceed 50 characters.")]
        public string Airline { get; set; } = null!;

        /// <summary>
        /// Gets or sets the IATA or ICAO code of the departure airport.
        /// </summary>
        [Required(ErrorMessage = "DepartureAirport is required.")]
        [StringLength(5, MinimumLength = 3, ErrorMessage = "DepartureAirport must be 3-5 characters.")]
        public string DepartureAirport { get; set; } = null!;

        /// <summary>
        /// Gets or sets the IATA or ICAO code of the arrival airport.
        /// </summary>
        [Required(ErrorMessage = "ArrivalAirport is required.")]
        [StringLength(5, MinimumLength = 3, ErrorMessage = "ArrivalAirport must be 3-5 characters.")]
        public string ArrivalAirport { get; set; } = null!;

        /// <summary>
        /// Gets or sets the scheduled departure time of the flight.
        /// </summary>
        [Required(ErrorMessage = "DepartureTime is required.")]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the scheduled arrival time of the flight.
        /// Must be after <see cref="DepartureTime"/>.
        /// </summary>
        [Required(ErrorMessage = "ArrivalTime is required.")]
        [DateGreaterThan("DepartureTime", ErrorMessage = "ArrivalTime must be after DepartureTime.")]
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the current status of the flight.
        /// </summary>
        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(FlightStatus), ErrorMessage = "Invalid FlightStatus value.")]
        public FlightStatus Status { get; set; }
    }
}
