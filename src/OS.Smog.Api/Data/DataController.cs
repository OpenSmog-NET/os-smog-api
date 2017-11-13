using MediatR;
using Microsoft.AspNetCore.Mvc;
using OS.Core;
using OS.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    /// <summary>
    /// </summary>
    [Route("v1/[controller]")]
    public class DataController : Controller
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">The mediator</param>
        public DataController(IMediator mediator)
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
        /// <param name="data">Body encoded list of device measurement objects</param>
        /// <response code="200">The payload has been uploaded successfully</response>
        /// <response code="400">Failed to deserialize the request body, or the payload validation has failed</response>
        /// <response code="500">Failed to persist the data</response>
        /// <returns>Empty response</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        [ProducesResponseType(typeof(ApiResult), 500)]
        public async Task<IActionResult> Data(Guid id, [FromBody] Measurements data)
        {
            var response = await mediator.Send(new ValidateMeasurementsCommand(id, data));

            switch (response.HasError)
            {
                case true when response.Errors.Any(x => x.Type == ApiErrorType.Validation):
                    return BadRequest(response);
            }

            return Ok(response);
        }
    }
}