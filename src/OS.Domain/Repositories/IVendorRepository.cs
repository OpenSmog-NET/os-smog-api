using OS.Domain.Queries;

namespace OS.Domain.Repositories
{
    public interface IVendorRepository
    {
        long Insert(Vendor vendor);

        Vendor Get(long id);

        QueryResult<Vendor> Get(Query query);
    }
}