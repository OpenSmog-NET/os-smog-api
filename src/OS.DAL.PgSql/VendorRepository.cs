using OS.DAL.PgSql.Model;
using OS.Domain.Queries;
using OS.Domain.Repositories;

namespace OS.DAL.PgSql
{
    public class VendorRepository : Repository<DeviceDbContext, Vendor, long>, IVendorRepository
    {
        private IEntityMapper<Domain.Vendor, Vendor> mapper;

        public VendorRepository(DeviceDbContext context, IEntityMapper<Domain.Vendor, Vendor> mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public long Create(Domain.Vendor vendor)
        {
            //todo: implement basic CR(ud) methods
            return Insert(mapper.MapToModel(vendor));
        }

        public Domain.Vendor Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public QueryResult<Domain.Vendor> Get(Query query)
        {
            throw new System.NotImplementedException();
        }
    }
}