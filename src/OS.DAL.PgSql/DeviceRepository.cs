using OS.DAL.PgSql.Model;
using OS.Domain.Repositories;
using System;

namespace OS.DAL.PgSql
{
    public class DeviceRepository : Repository<DeviceDbContext, Device, Guid>, IDeviceRepository
    {
        private readonly IEntityMapper<Domain.Device, Device> mapper;

        public DeviceRepository(DeviceDbContext context, IEntityMapper<Domain.Device, Device> mapper)
            : base(context)
        {
            this.mapper = mapper;
        }

        public Guid Insert(Domain.Device device)
        {
            return Insert(mapper.MapToModel(device));
        }

        public Domain.Device Get(Guid id)
        {
            return Get(id, mapper.MapFromModel);
        }
    }
}