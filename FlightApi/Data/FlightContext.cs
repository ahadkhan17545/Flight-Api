using FlightApi.Models;
using Microsoft.EntityFrameworkCore;
namespace FlightApi.Data
{
    /// <summary>  
    /// Represents the database context for managing flight-related data.  
    /// </summary>  
    public class FlightContext : DbContext
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="FlightContext"/> class with the specified options.  
        /// </summary>  
        /// <param name="options">The options to configure the database context.</param>  
        public FlightContext(DbContextOptions<FlightContext> options) : base(options) { }

        /// <summary>  
        /// Gets or sets the <see cref="DbSet{Flight}"/> representing the flights in the database.  
        /// </summary>  
        public DbSet<Flight> Flights { get; set; }
    }
}