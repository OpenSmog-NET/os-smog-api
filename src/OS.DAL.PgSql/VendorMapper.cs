using OS.Domain;
using System.Linq;
using Device = OS.DAL.PgSql.Model.Device;
using Vendor = OS.DAL.PgSql.Model.Vendor;
using VendorApiKey = OS.DAL.PgSql.Model.VendorApiKey;

namespace OS.DAL.PgSql
{
    public class VendorMapper : IEntityMapper<Domain.Vendor, Model.Vendor>
    {
        public Vendor MapToModel(Domain.Vendor @object)
        {
            return new Vendor()
            {
                Name = @object.Name,
                Url = @object.Url,
                Keys = @object.Keys.Select(x => new VendorApiKey()
                {
                    Id = x.Id,
                    Key = x.Key,
                    Limit = x.Limit
                }).ToList(),
                Devices = @object.Devices.Select(x => new Device()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = (int)x.Type,
                    Lat = x.Location.Lat,
                    Lon = x.Location.Lon
                }).ToList()
            };
        }

        public Domain.Vendor MapFromModel(Vendor entity)
        {
            return new Domain.Vendor()
            {
                Name = entity.Name,
                Url = entity.Url,
                Keys = entity.Keys.Select(x => new Domain.VendorApiKey()
                {
                    Id = x.Id,
                    Key = x.Key,
                    Limit = x.Limit
                }).ToList(),
                Devices = entity.Devices.Select(x => new Domain.Device()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = (DeviceType)x.Type,
                    Location = new Location()
                    {
                        Lat = x.Lat,
                        Lon = x.Lon
                    }
                }).ToList()
            };
        }
    }
}