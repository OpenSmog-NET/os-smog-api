using MediatR;
using Microsoft.AspNetCore.Mvc;
using OS.Core;
using OS.Smog.Domain.Sensors;
using OS.Smog.Dto.Sensors;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Smog.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("v1/[controller]")]
    public class SensorsController : Controller
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator</param>
        public SensorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// POST Measurements from a device
        /// </summary>
        /// <remarks>
        /// Requires Content-Type application/json request header
        /// </remarks>
        /// <param name="id">Device Id (SUID)</param>
        /// <param name="payload">Body encoded list of device measurement objects</param>
        /// <response code="200">The payload has been uploaded successfully</response>
        /// <response code="400">Failed to deserialize the request body, or the payload validation has failed</response>
        /// <response code="500">Failed to persist the data</response>
        /// <returns>Empty response</returns>
        [HttpPost("{id}/data")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        [ProducesResponseType(typeof(ApiResult), 500)]
        public async Task<IActionResult> Data(Guid id, [FromBody] Payload payload)
        {
            var response = await mediator.Send(new UploadMeasurementsCommand(payload));

            switch (response.HasError)
            {
                case true when response.Errors.Any(x => x.Type == ApiErrorType.Validation):
                    return BadRequest(response);
            }

            return Ok(response);
        }
    }
}