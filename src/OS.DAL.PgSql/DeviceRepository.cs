using OS.DAL.PgSql.Model;
using OS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IList<Guid> Insert(IList<Domain.Device> devices)
        {
            return Insert(devices.Select(v => mapper.MapToModel(v)).ToList());
        }

        public Domain.Device Get(Guid id)
        {
            return Get(id, mapper.MapFromModel);
        }
    }
}