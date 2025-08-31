using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FlightApi.Controllers
{
    /// <summary>
    /// Controller responsible for handling application errors and returning standardized error responses.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="logger">The logger used to record error details.</param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles unhandled exceptions and returns a problem details response with error information.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the problem details with error information and a correlation ID.
        /// </returns>
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            // Log the exception with stack trace
            if (exception != null)
            {
                _logger.LogError(exception, "Unhandled exception occurred with TraceId {TraceId}", HttpContext.TraceIdentifier);
            }

            // Include correlation ID (traceId) in the ProblemDetails response
            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred",
                Detail = exception?.Message,
                Status = 500,
                Instance = HttpContext.TraceIdentifier // Correlation ID
            };

            return Problem(
                detail: problemDetails.Detail,
                statusCode: problemDetails.Status,
                title: problemDetails.Title,
                instance: problemDetails.Instance
            );
        }
    }
}
