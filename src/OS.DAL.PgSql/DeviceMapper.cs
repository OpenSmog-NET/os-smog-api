using OS.Domain;
using Device = OS.DAL.PgSql.Model.Device;

namespace OS.DAL.PgSql
{
    public class DeviceMapper : IEntityMapper<Domain.Device, Device>
    {
        public Device MapToModel(Domain.Device @object)
        {
            return new Device()
            {
                Id = @object.Id,
                VendorId = @object.VendorId,
                Name = @object.Name,
                Type = (int)@object.Type,
                Lon = @object.Location.Lon,
                Lat = @object.Location.Lat
            };
        }

        public Domain.Device MapFromModel(Device entity)
        {
            return new Domain.Device()
            {
                Id = entity.Id,
                VendorId = entity.VendorId,
                Name = entity.Name,
                Type = (DeviceType)entity.Type,
                Location = new Location()
                {
                    Lon = entity.Lon,
                    Lat = entity.Lat
                }
            };
        }
    }
}