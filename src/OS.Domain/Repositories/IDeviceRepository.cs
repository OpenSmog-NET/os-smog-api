using OS.Core.Queries;
using System;

namespace OS.Domain.Repositories
{
    public interface IDeviceRepository
    {
        Guid Insert(Domain.Device device);

        Domain.Device Get(Guid id);

        QueryResult<Domain.Device> Get(Query query = null);

        long Count(Query query = null);
    }
}