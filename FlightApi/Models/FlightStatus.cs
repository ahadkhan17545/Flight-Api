namespace FlightApi.Models
{
    /// <summary>
    /// Represents the status of a flight.
    /// </summary>
    public enum FlightStatus
    {
        /// <summary>
        /// The flight is scheduled.
        /// </summary>
        Scheduled,

        /// <summary>
        /// The flight is delayed.
        /// </summary>
        Delayed,

        /// <summary>
        /// The flight is cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// The flight is currently in the air.
        /// </summary>
        InAir,

        /// <summary>
        /// The flight has landed.
        /// </summary>
        Landed
    }
}