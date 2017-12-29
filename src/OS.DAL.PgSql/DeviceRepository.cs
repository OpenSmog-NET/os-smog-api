using OS.Core.Queries;
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

        public long Count(Query query = null)
        {
            return Count(this.Query);
        }

        public Domain.Device Get(Guid id)
        {
            return Get(id, mapper.MapFromModel);
        }

        public QueryResult<Domain.Device> Get(Query query = null)
        {
            return Get(Query, query ?? new Query(), mapper.MapFromModel);
        }

        public Guid Insert(Domain.Device device)
        {
            return Insert(mapper.MapToModel(device));
        }

        public IList<Guid> Insert(IList<Domain.Device> devices)
        {
            return Insert(devices.Select(v => mapper.MapToModel(v)).ToList());
        }
    }
}