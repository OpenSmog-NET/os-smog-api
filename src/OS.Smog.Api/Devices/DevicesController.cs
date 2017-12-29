using Microsoft.AspNetCore.Mvc;
using OS.Core;
using OS.Core.Queries;
using OS.Domain;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;

namespace OS.Smog.Api.Devices
{
    /// <summary>
    /// </summary>
    [Route("v1/[controller]")]

    public class DevicesController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        /// <summary>
        /// Returns optionally filtered list of devices.
        /// </summary>
        /// <remarks>
        /// ## Description
        /// Build urls using following convention.
        ///
        /// ## Request Filtering
        ///
        /// ### By device name (exact match)
        ///
        /// /v1/devices?filter=name eq 'ABC'
        ///
        /// ### By device name (similar match)
        /// /v1/devices?filter=name lk 'ABC'
        ///
        /// ### By device country
        /// /v1/devices?filter=address.countryCode eq 'PL'
        ///
        /// ### By device location
        /// /v1/devices?filter=location.lon gt 20 and location.lon lt 25
        ///
        /// ## Filter Syntax
        /// - eq (Equals)
        /// - gt (Greater then)
        /// - ge (Greather then or equal
        /// - lt (Less then)
        /// - le (Less then or equal)
        /// - sw (Starts with)
        /// - lk (Like)
        /// - in (In)
        ///
        /// </remarks>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ApiResult<QueryResult<Device>>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ApiResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ApiResult))]
        public IActionResult Get(DevicesQuery query)
        {
            return Ok();
        }
    }
}