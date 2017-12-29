using OS.Core;
using OS.Core.Queries;
using OS.Domain;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Devices
{
    /// <inheritdoc />
    public class DevicesQuery : ApiQuery
    {
        private readonly Func<string, bool> nestedPropertyFilter = (property) =>
        {
            var allowedProperties = new List<string>
            {
                $"{nameof(Device.Location)}.{nameof(Location.Lon)}",
                $"{nameof(Device.Location)}.{nameof(Location.Lat)}",
                $"{nameof(Device.Address)}.{nameof(PostalAddress.CountryCode)}",
                $"{nameof(Device.Address)}.{nameof(PostalAddress.State)}",
                $"{nameof(Device.Address)}.{nameof(PostalAddress.City)}",
            };

            return allowedProperties.Exists(x => x.Equals(property, StringComparison.OrdinalIgnoreCase));
        };

        /// <inheritdoc />
        public override Query GetQuery()
        {
            return base.GetQuery<Domain.Device>(nestedPropertyFilter: nestedPropertyFilter);
        }
    }
}