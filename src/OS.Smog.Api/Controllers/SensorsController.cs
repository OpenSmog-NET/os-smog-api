using MediatR;
using Microsoft.AspNetCore.Mvc;
using OS.Smog.Domain.Sensors;
using OS.Smog.Dto.Sensors;
using System;
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
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("{id}/data")]
        public async Task<IActionResult> Data(Guid id, [FromBody]Payload payload)
        {
            var response = await mediator.Send(new UploadMeasurementsCommand(payload));

            return StatusCode((int)response);
        }
    }
}