using Microsoft.EntityFrameworkCore;
using OS.Core.Queries;
using OS.DAL.PgSql.Model;
using OS.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OS.DAL.PgSql
{
    public class VendorRepository : Repository<DeviceDbContext, Vendor, long>, IVendorRepository
    {
        private readonly IEntityMapper<Domain.Vendor, Vendor> mapper;

        public VendorRepository(DeviceDbContext context, IEntityMapper<Domain.Vendor, Vendor> mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public long Insert(Domain.Vendor vendor)
        {
            return Insert(mapper.MapToModel(vendor));
        }

        public IList<long> Insert(IList<Domain.Vendor> vendors)
        {
            return Insert(vendors.Select(v => mapper.MapToModel(v)).ToList());
        }

        public Domain.Vendor Get(long id)
        {
            return Get(id, mapper.MapFromModel);
        }

        public QueryResult<Domain.Vendor> Get(Query query = null)
        {
            return Get(Query, query ?? new Query(), mapper.MapFromModel);
        }

        protected override IQueryable<Vendor> Query => base.Query
            .Include(x => x.Keys);
    }
}